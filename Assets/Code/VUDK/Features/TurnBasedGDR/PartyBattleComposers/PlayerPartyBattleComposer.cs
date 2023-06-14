namespace VUDK.Features.TurnBasedGDR.CombatSystem.PartyComposers
{
    using UnityEngine;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Pools;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;

    public class PlayerPartyBattleComposer : PartyBattleComposer<PlayerUnitManager>
    {
        [SerializeField]
        private CardsPool _cardsPool;
        [SerializeField, Header("Container")]
        private RectTransform _handsContainer;

        protected override void GenerateUnit(PlayerUnitManager unit, UnitData unitData, Party relatedParty, Vector3 position)
        {
            unit.Init(unitData, relatedParty, Pool, _cardsPool, _handsContainer);
            InFightUnitsManager.Add(unit);
            SetUnitBattleName(unit);
            SetUnitBattlePosition(unit, position);
        }
    }
}