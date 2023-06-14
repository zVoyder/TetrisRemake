namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{
    using UnityEngine;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Pools;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units.CardsSystem;

    [RequireComponent(typeof(UnitHand))]
    [RequireComponent(typeof(UnitDeck))]
    public class PlayerUnitManager : UnitManager
    {
        public UnitHand UnitHand { get; private set; }
        public UnitDeck UnitDeck { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out UnitHand hand);
            TryGetComponent(out UnitDeck deck);
            UnitHand = hand;
            UnitDeck = deck;
        }

        /// <summary>
        /// Initializes this <see cref="UnitManager"/>.
        /// </summary>
        /// <param name="unitData"><see cref="UnitData"/> of the unit.</param>
        /// <param name="party"><see cref="Party"/> of the unit.</param>
        /// <param name="pool">Related <see cref="Pool"/>.</param>
        /// <param name="cardsPool">Related <see cref="CardsPool"/></param>
        /// <param name="handRectTransform"><see cref="RectTransform"/> where to generate the <see cref="UnitHand"/> in.</param>
        public void Init(UnitData data, Party relatedParty, Pool associatedPool, CardsPool cardsPool, RectTransform handRectTransform)
        {
            base.Init(data, relatedParty, associatedPool);
            UnitDeck.Init(data.Skills);
            UnitHand.Init($"{UnitData.UnitName} Hand", handRectTransform, this, UnitDeck, cardsPool);
        }
    }
}