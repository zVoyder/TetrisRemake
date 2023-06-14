namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using System;
    using Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.PartySystem;

    public class CheckFightConditionCombatTurnState : LinkedTurnState
    {
        private PlayerParty _playerParty;
        private EnemyParty _enemyParty;

        private event Action _onPlayerPartyWin;
        private event Action _onEnemyPartyWin;

        public CheckFightConditionCombatTurnState(
            string name,
            TurnStateMachine relatedStateMachine,
            PlayerParty playerParty,
            EnemyParty enemyParty,
            Action onPlayerPartyWin,
            Action onEnemyPartyWin) : base(name, relatedStateMachine)
        {
            _playerParty = playerParty;
            _enemyParty = enemyParty;
            _onPlayerPartyWin = onPlayerPartyWin;
            _onEnemyPartyWin = onEnemyPartyWin;
        }

        public override void Enter()
        {
            if (!_playerParty.AreThereMembersAlive())
            {
                _onEnemyPartyWin?.Invoke();
                return;
            }

            if (!_enemyParty.AreThereMembersAlive())
            {
                _onPlayerPartyWin?.Invoke();
                return;
            }

            RelatedStateMachine.NextState();
        }

        public override void Exit()
        {

        }

        public override void Process()
        {
        }
    }
}