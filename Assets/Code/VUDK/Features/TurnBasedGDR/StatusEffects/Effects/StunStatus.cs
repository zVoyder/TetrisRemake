namespace VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects
{
    using UnityEngine;
    using System.Collections;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public class StunStatus : StatusEffectBase
    {
        public StunStatus(StatusEffectData data) : base(data)
        {
        }

        public override void Enter()
        {
            UnitManagerTarget.UnitStatusEffects.ApplyStun();
        }

        public override void Exit()
        {
            UnitManagerTarget.UnitStatusEffects.RemoveStun();
        }

        public override void Process()
        {
            base.Process();
        }
    }
}