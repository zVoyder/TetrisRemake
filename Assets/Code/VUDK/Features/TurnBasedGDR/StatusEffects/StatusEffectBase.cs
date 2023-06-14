namespace VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects
{
    using VUDK.Patterns.StateMachine.Interfaces;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public class StatusEffectBase : IEventState 
    {
        protected UnitManager UnitManagerTarget;
        protected AttacksManager AttacksManager;

        public StatusEffectData Data { get; protected set; }
        public int AppliedTurns { get; protected set; }

        public StatusEffectBase(StatusEffectData data)
        {
            Data = data;
            AppliedTurns = Data.AppliedTurns;
        }

        public void Init(UnitManager unitTarget, AttacksManager attacksManager)
        {
            AttacksManager = attacksManager;
            UnitManagerTarget = unitTarget;
        }

        /// <summary>
        /// On Apply status effect event.
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        /// On Process status effect event.
        /// </summary>
        public virtual void Process()
        {
            AppliedTurns--;
        }

        /// <summary>
        /// On Remove status effect event.
        /// </summary>
        public virtual void Exit() { }

    }
}