namespace VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects
{
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public class DamageDownStatus : StatusEffectBase
    {
        public DamageDownStatus(StatusEffectData data) : base(data)
        {
        }

        public override void Enter()
        {
            UnitManagerTarget.UnitStatusEffects.ApplyDamageDown(AttacksManager.DamageDown);
        }

        public override void Exit()
        {
            UnitManagerTarget.UnitStatusEffects.RemoveDamageDown();
        }

        public override void Process()
        {
            base.Process();
        }
    }
}