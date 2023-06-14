namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;

    public class EnemyUnitActionState : UnitState<EnemyUnitManager>
    {
        public EnemyUnitActionState(string name, TurnStateMachine relatedStateMachine, EnemyUnitManager unit) : base(name, relatedStateMachine, unit) // I will pass here the "cards drawer".
        {
        }

        public override void Enter()
        {
#if DEBUG
            Debug.Log("---- ENEMY ATTACK STATE");
#endif
            UnitManager.EnemyAI.AttackRandomTarget();
            RelatedStateMachine.NextStateIn(2f);
        }

        public override void Exit()
        {
        }

        public override void Process()
        {
        }
    }
}