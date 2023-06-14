namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using System.Collections;
    using System.Collections.Generic;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using UnityEngine;

    public class EnemyCombatTurnState : LinkedTurnState
    {
        private EnemyPartyTurns _partyTurns;
        private EnemyParty _party;

        public EnemyCombatTurnState(string name, TurnStateMachine relatedStateMachine, EnemyPartyTurns partyTurns, EnemyParty enemyParty) : base(name, relatedStateMachine)
        {
            _party = enemyParty;
            _partyTurns = partyTurns;
            _partyTurns.InitStates(enemyParty, relatedStateMachine);
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log($"--ENTERING STATEMACHINE OF {_partyTurns.transform.name}");
#endif
            _partyTurns.Begin();
        }

        public override void Exit()
        {
        }

        public override void Process()
        {
        }
    }
}