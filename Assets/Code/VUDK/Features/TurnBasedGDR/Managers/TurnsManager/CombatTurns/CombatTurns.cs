namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using System;
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.PartySystem;

    public class CombatTurns : TurnStateMachine
    {
        [SerializeField, Range(0, 100)]
        private sbyte _playerChanceToBegin = 50;

        /// <summary>
        /// Initializes the states of the <see cref="CombatTurns"/>.
        /// </summary>
        /// <param name="playerPartyTurns">Turns of the player's party.</param>
        /// <param name="enemyPartyTurns">Turns of the enemy's party.</param>
        /// <param name="playerParty"><see cref="Party"/> of the player.</param>
        /// <param name="enemyParty"><see cref="Party"/> of the enemy.</param>
        /// <param name="onPlayerWin">Event on player win.</param>
        /// <param name="onEnemyWin">Event on enemy win.</param>
        public void InitStates(
            PlayerPartyTurns playerPartyTurns, EnemyPartyTurns enemyPartyTurns,
            PlayerParty playerParty, EnemyParty enemyParty,
            Action onPlayerWin, Action onEnemyWin)
        {
            base.InitStates();

            State combatTurnPlayerParty = new CombatTurnState<PlayerParty, PlayerPartyTurns>("PlayerPartyTurn", this, playerParty, playerPartyTurns);
            State combatTurnEnemyParty = new CombatTurnState<EnemyParty, EnemyPartyTurns>("EnemyPartyTurn", this, enemyParty, enemyPartyTurns);

            if (_playerChanceToBegin >= UnityEngine.Random.Range(0, 101)) 
            {
                States.Add(combatTurnPlayerParty);
                States.Add(combatTurnEnemyParty);
            }
            else
            {
                States.Add(combatTurnEnemyParty);
                States.Add(combatTurnPlayerParty);
            }

            States.Add(new CheckFightConditionCombatTurnState("CheckFightCondition", this, playerParty, enemyParty, onPlayerWin, onEnemyWin));
        }
    }
}