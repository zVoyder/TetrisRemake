namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Patterns.StateMachine;

    public class PlayerPartyTurnState : PartyTurnState
    {
        public PlayerPartyTurnState(string name, TurnStateMachine relatedStateMachine, PlayerUnitManager relatedUnitManager) : base(name, relatedStateMachine, relatedUnitManager)
        {
            (relatedUnitManager.UnitTurns as PlayerUnitTurns).InitStates(relatedStateMachine, relatedUnitManager);
        }
    }
}
