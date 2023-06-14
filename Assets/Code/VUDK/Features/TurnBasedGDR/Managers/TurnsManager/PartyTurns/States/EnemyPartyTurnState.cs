namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Patterns.StateMachine;

    public class EnemyPartyTurnState : PartyTurnState
    {
        public EnemyPartyTurnState(string name, TurnStateMachine relatedStateMachine, EnemyUnitManager relatedUnitManager) : base(name, relatedStateMachine, relatedUnitManager)
        {
            (relatedUnitManager.UnitTurns as EnemyUnitTurns).InitStates(relatedStateMachine, relatedUnitManager);
        }
    }
}
