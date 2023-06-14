namespace VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects
{
    using UnityEngine;
    using System.Collections;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.SOData.Structures.Enums;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public class BleedStatus : StatusEffectBase
    {
        public BleedStatus(StatusEffectData data) : base(data)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
            // No needs to add a method for removing bleed since it's just only a TakeDamage method in the Process
        }

        public override void Process()
        {
            base.Process();
            UnitManagerTarget.UnitStatusEffects.ApplyBleedDamage(AttacksManager.BleedDamage);;
        }
    }
}