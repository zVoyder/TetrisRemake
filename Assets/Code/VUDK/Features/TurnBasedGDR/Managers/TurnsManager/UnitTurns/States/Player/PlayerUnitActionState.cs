namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;

    public class PlayerUnitActionState : UnitState<PlayerUnitManager>
    {
        public PlayerUnitActionState(string name, TurnStateMachine relatedStateMachine, PlayerUnitManager unit) : base(name, relatedStateMachine, unit) // I will pass here the "cards drawer".
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log("---- PLAYER ACTION STATE");
#endif
            RelatedStateMachine.NextStateIn(1f);
        }

        public override void Exit()
        {
        }

        public override void Process()
        {
        }
    }
}