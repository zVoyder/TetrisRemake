namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.PartySystem;

    public class PlayerPartyTurns : PartyTurns<PlayerParty>
    {
        public override void InitStates(PlayerParty playerParty, TurnStateMachine parentStateMachine)
        {
            base.InitStates(playerParty, parentStateMachine);

            foreach (PlayerUnitManager unitManager in Party.GetComposedUnits())
            {
                States.Add(new PlayerPartyTurnState(unitManager.UnitData.UnitName, this, unitManager));
            }
            States.Add(new EndSubStateMachine("EndPlayerPartyPhase", this, parentStateMachine));
        }
    }
}