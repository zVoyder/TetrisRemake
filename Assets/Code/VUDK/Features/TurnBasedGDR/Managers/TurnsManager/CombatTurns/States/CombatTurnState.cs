namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using UnityEngine;

    public class CombatTurnState<T1, T2> : LinkedTurnState where T1 : Party where T2 : PartyTurns<T1>
    {
        private T1 _party;
        private T2 _partyTurns;

        public CombatTurnState(string name, TurnStateMachine relatedStateMachine, T1 party, T2 partyTurns) : base(name, relatedStateMachine)
        {
            _party = party;
            _partyTurns = partyTurns;
            _partyTurns.InitStates(party, relatedStateMachine);
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
