namespace VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects
{
    using UnityEngine;
    using System.Collections;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public class FearStatus : StatusEffectBase
    {
        public FearStatus(StatusEffectData data) : base(data)
        {
        }

        public override void Enter()
        {
            UnitManagerTarget.UnitStatusEffects.ApplyFear(AttacksManager.ReceiveDamageReduction);
        }

        public override void Exit()
        {
            UnitManagerTarget.UnitStatusEffects.RemoveFear();
        }

        public override void Process()
        {
            base.Process();
        }
    }
}