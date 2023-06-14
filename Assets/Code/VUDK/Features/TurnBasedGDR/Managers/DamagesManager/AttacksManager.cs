namespace VUDK.Features.TurnBasedGDR.CombatSystem.Managers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects.Factory;
    using VUDK.Features.TurnBasedGDR.SOData.Structures.Enums;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects;

    public class AttacksManager : MonoBehaviour
    {
        [SerializeField, Header("Statistics")]
        private int _criticalMultiplier = 2;

        [field: SerializeField, Range(0, 100), Header("Status Effects Statistics")]
        public sbyte ReceiveDamageReduction {  get; private set; }
        [field: SerializeField, Range(0, 100)]
        public sbyte DamageDown { get; private set; }
        [field: SerializeField, Range(0, 100)]
        public sbyte DamageUp { get; private set; }

        [field: SerializeField, Min(0)]
        public int BleedDamage { get; private set; }

        /// <summary>
        /// Uses a <see cref="SkillData"/> on a <see cref="UnitManager"/>.
        /// </summary>
        /// <param name="unitAttacker">Unit Attacker.</param>
        /// <param name="skill">Skill.</param>
        /// <param name="unitReceiver">Unit receiver.</param>
        public void UseSkillOnUnit(UnitManager unitAttacker, SkillData skill, UnitManager unitReceiver)
        {
            int randomPower = CalculateDamage(skill, unitAttacker, unitReceiver, out bool hasSucceeded);

            Action<UnitManager> _onSkillAffect = null;

            switch (skill.SkillType)
            {
                case SkillType.Heal:
                    _onSkillAffect += (receiver) =>
                    {
                        receiver.Unit.HealHitPoints(randomPower);
                        if (hasSucceeded)
                            receiver.ReceiveCritical();
                    };
                    break;
                case SkillType.Damage:
                    _onSkillAffect += (receiver) =>
                    {
                        receiver.Unit.TakeDamage(randomPower);
                        if(hasSucceeded)
                            receiver.ReceiveCritical();
                        receiver.UnitAnimatorController.AnimGetHit();
                        receiver.UnitAudioHandler.PlayHurtClip();
                    };
                    break;
            }

            unitAttacker.UnitAnimatorController.AnimSkill(skill);
            unitAttacker.UnitAudioHandler.PlayClip(skill.AttackClip);

            if (skill.IsAreaOfEffect)
            {
                AffectGroupWithSkill(skill, unitReceiver.Party, _onSkillAffect);
                return;
            }

            AffectUnitWithSkill(skill, unitReceiver, _onSkillAffect);
        }

        /// <summary>
        /// Rolls the critical chance of the <see cref="SkillData"/>.
        /// </summary>
        /// <param name="skill">Skill to use.</param>
        /// <returns>One on failure, Critical Multiplier of Skill Data on success.</returns>
        private int RollCriticalChance(SkillData skill, UnitManager unitReceiver, out bool hasSucceeded)
        {
            if (skill.CriticalChance >= UnityEngine.Random.Range(0, 101))
            {
                hasSucceeded = true;
                return _criticalMultiplier;
            }

            hasSucceeded = false;
            return 1;
        }

        /// <summary>
        /// Applies the status effects to a <see cref="UnitManager"/>.
        /// </summary>
        /// <param name="effectsData">List of <see cref="StatusEffectData"/> to apply to a <see cref="UnitManager"/>.</param>
        /// <param name="unitReceiver"></param>
        private void ApplyEffectsStatus(List<StatusEffectData> effectsData, UnitManager unitReceiver)
        {
            foreach(StatusEffectData effectData in effectsData)
            {
                StatusEffectBase effect = effectData.Construct();
                effect.Init(unitReceiver, this);
                unitReceiver.UnitStatusEffects.AddStatusEffect(effect);
            }
        }

        /// <summary>
        /// Calculates the damage to apply to a <see cref="UnitManager"/>
        /// </summary>
        /// <param name="skill"><see cref="SkillData"/> to use on the receiver.</param>
        /// <param name="unitAttacker">Unit Attacker.</param>
        /// <param name="unitReceiver">Unit Receiver.</param>
        /// <param name="hasSucceeded">True if the critical roll has succeeded, False if not.</param>
        /// <returns>The calculated damage.</returns>
        private int CalculateDamage(SkillData skill, UnitManager unitAttacker, UnitManager unitReceiver, out bool hasSucceeded)
        {
            int power = skill.Power.Random() * RollCriticalChance(skill, unitReceiver, out hasSucceeded);
            power *= 1 + unitAttacker.UnitStatusEffects.CurrentDamageUp / 100;
            power *= 1 - unitAttacker.UnitStatusEffects.CurrentDamageDown / 100;
            return power;
        }

        /// <summary>
        /// Affect a <see cref="UnitManager"/> with a <see cref="SkillData"/>.
        /// </summary>
        /// <param name="skill">Skill to affect the unit with.</param>
        /// <param name="unitReceiver">Unit receiver.</param>
        /// <param name="onSkillAffect">Event on skill affect.</param>
        private void AffectUnitWithSkill(SkillData skill, UnitManager unitReceiver, Action<UnitManager> onSkillAffect)
        {
            onSkillAffect.Invoke(unitReceiver);
            ApplyEffectsStatus(skill.SkillStatusEffects, unitReceiver);
        }

        /// <summary>
        /// Affects an entire <see cref="Party"/> with a <see cref="SkillData"/>.
        /// </summary>
        /// <param name="skill">Skill to use.</param>
        /// <param name="partyReceiver">Party receiver.</param>
        /// <param name="onSkillAffect">Event on skill affect.</param>
        private void AffectGroupWithSkill(SkillData skill, Party partyReceiver, Action<UnitManager> onSkillAffect)
        {
            List<UnitManager> units = partyReceiver.GetComposedUnits().ToList();

            for (int i = 0; i < units.Count; i++)
            {
                AffectUnitWithSkill(skill, units[i], onSkillAffect);
            }
        }
    }
}