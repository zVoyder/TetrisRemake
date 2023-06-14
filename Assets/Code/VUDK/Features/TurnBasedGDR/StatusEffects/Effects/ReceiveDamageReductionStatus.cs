namespace VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects
{
    using UnityEngine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.SOData;

    public class DamageReductionStatus : StatusEffectBase
    {
        public DamageReductionStatus(StatusEffectData data) : base(data)
        {
        }

        public override void Enter()
        {
            UnitManagerTarget.UnitStatusEffects.ApplyDamageReduction(AttacksManager.ReceiveDamageReduction);
        }

        public override void Exit()
        {
            UnitManagerTarget.UnitStatusEffects.RemoveDamageReduction();
        }

        public override void Process()
        {
            base.Process();
        }
    }
}