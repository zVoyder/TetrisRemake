namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using VUDK.Patterns.StateMachine;

    public class EndSubStateMachine : LinkedState
    {
        public TurnStateMachine ParentStateMachine { get; private set; }

        public EndSubStateMachine(string name, TurnStateMachine relatedStateMachine, TurnStateMachine parentStateMachine) : base(name, parentStateMachine)
        {
            ParentStateMachine = parentStateMachine;
        }

        public override void Enter()
        {
            ParentStateMachine.NextState();
        }

        public override void Exit()
        {
        }

        public override void Process()
        {
        }
    }
}