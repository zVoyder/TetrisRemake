namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;

    public class EnemyUnitTurns : UnitTurns
    {
        public void InitStates(TurnStateMachine parentStateMachine, EnemyUnitManager unitManager)
        {
            base.InitStates(parentStateMachine);
            States.Add(new EnemyUnitActionState("EnemyAttackState", this, unitManager));
            States.Add(new EndSubStateMachine("EndEnemyUnitPhase", this, parentStateMachine));
        }
    }
}
