namespace VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines
{
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnemyPartyTurns : PartyTurns<EnemyParty>
    {
        public override void InitStates(EnemyParty enemyParty, TurnStateMachine parentStateMachine)
        {
            base.InitStates(enemyParty, parentStateMachine);

            foreach (EnemyUnitManager unitManager in Party.GetComposedUnits())
            {
                States.Add(new EnemyPartyTurnState(unitManager.UnitData.UnitName, this, unitManager));
            }
            States.Add(new EndSubStateMachine("EndEnemyPartyPhase", this, parentStateMachine));
        }
    }
}