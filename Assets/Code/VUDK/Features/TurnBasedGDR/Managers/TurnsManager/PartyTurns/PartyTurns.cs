namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using System.Collections.Generic;
    using UnityEngine;

    public class PartyTurns<T> : SubTurnStateMachine where T : Party
    {
        protected T Party;

        /// <summary>
        /// Initializes the states based on how many <see cref="UnitManager"/> has the <see cref="Party"/>.
        /// </summary>
        /// <param name="enemyParty"><see cref="Party"/> of the enemy.</param>
        /// <param name="parentStateMachine">Parent <see cref="TurnStateMachine"/>.</param>
        public virtual void InitStates(T party, TurnStateMachine parentStateMachine)
        {
            Party = party;
            InitStates(parentStateMachine);
        }

#if DEBUG
        [ContextMenu("Next State")]
        private void DebugNextState()
        {
            NextState();
        }
#endif
    }
}