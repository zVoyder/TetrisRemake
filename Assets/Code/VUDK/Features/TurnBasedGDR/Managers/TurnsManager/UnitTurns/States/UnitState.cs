namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;

    public abstract class UnitState<T> : LinkedTurnState where T : UnitManager
    {
        protected T UnitManager;

        public UnitState(string name, TurnStateMachine relatedStateMachine, T unitManager) : base(name, relatedStateMachine) // I will pass here the "cards drawer".
        {
            UnitManager = unitManager;
        }
    }
}