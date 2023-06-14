namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;

    public abstract class UnitTurns : SubTurnStateMachine
    {
        /// <summary>
        /// Initializes the unit turns of a <see cref="UnitManager"/>.
        /// </summary>
        /// <param name="parentStateMachine">Parent <see cref="TurnStateMachine"/>.</param>
        /// <param name="unitManager">Unit.</param>
        public virtual void InitStates(TurnStateMachine parentStateMachine, UnitManager unitManager)
        {
            base.InitStates(parentStateMachine);
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
