namespace VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects
{
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public class DamageUpStatus : StatusEffectBase
    {
        public DamageUpStatus(StatusEffectData data) : base(data)
        {
        }

        public override void Enter()
        {
            UnitManagerTarget.UnitStatusEffects.ApplyDamageUp(AttacksManager.DamageUp);
        }

        public override void Exit()
        {
            UnitManagerTarget.UnitStatusEffects.RemoveDamageUp();
        }

        public override void Process()
        {
            base.Process();
        }
    }
}