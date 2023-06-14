namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units.CardsSystem
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Pools;

    public class UnitHand : MonoBehaviour
    {
        [SerializeField]
        private GameObject _baseHandLayoutPrefab;
        private CardsPool _cardsPool;
        private UnitDeck _deck;
        private RectTransform _relatedHandRectTransform;
        private GameObject _handLayout;
        private string _handName;

        public UnitManager RelatedUnitManager { get; private set; }

        /// <summary>
        /// initializes this <see cref="UnitHand"/>.
        /// </summary>
        /// <param name="handName">Name of the hand.</param>
        /// <param name="relatedTransform"><see cref="RectTransform"/> where to generate the hand in.</param>
        /// <param name="relatedUnit">Related <see cref="UnitManager"/>.</param>
        /// <param name="deck">Related <see cref="UnitDeck"/>.</param>
        /// <param name="cardsPool">Related <see cref="CardsPool"/>.</param>
        public void Init(string handName, RectTransform relatedTransform, UnitManager relatedUnit, UnitDeck deck, CardsPool cardsPool)
        {
            _cardsPool = cardsPool;
            _handName = handName;
            _relatedHandRectTransform = relatedTransform;
            _deck = deck;
            RelatedUnitManager = relatedUnit;
            InstantiateHand();
        }

        /// <summary>
        /// Sets active the hand GameObject.
        /// </summary>
        /// <param name="active"></param>
        public void SetActiveHand(bool active)
        {
            if (!_handLayout)
                return;

            _handLayout.SetActive(active);
        }

        /// <summary>
        /// Generates a card by its <see cref="CardSkillData"/>.
        /// </summary>
        /// <param name="cardData">Data of the card.</param>
        public void GenerateCard(CardSkillData cardData)
        {
            GameObject cardGO = _cardsPool.Get();
            cardGO.transform.SetParent(_handLayout.transform);

            if (cardGO.TryGetComponent(out UnitCard card))
            {
                card.Init(cardData, this, _cardsPool);
            }
        }

        /// <summary>
        /// Instantiates this <see cref="UnitHand"/>.
        /// </summary>
        private void InstantiateHand()
        {
            _handLayout = Instantiate(_baseHandLayoutPrefab, _relatedHandRectTransform.position, Quaternion.identity, _relatedHandRectTransform);
            _handLayout.transform.name = _handName;
            SetUpSkipButton();
            GenerateCards();
            SetActiveHand(false);
        }

        /// <summary>
        /// Generates the cards.
        /// </summary>
        private void GenerateCards()
        {
            // Add method to add the skills to the deck

            foreach (CardSkillData skillData in _deck.CardDatas/*RelatedUnitManager.UnitData.Skills*/) // TO DO Deck cards + Data.Skills
            {
                GenerateCard(skillData);
            }
        }

        /// <summary>
        /// Setups the skip button.
        /// </summary>
        private void SetUpSkipButton()
        {
            if (TryGetSkipButton(out Button btn))
            {
                btn.onClick.AddListener(() => RelatedUnitManager.UnitTurns.NextState());
            }
        }

        /// <summary>
        /// Tries to get the button needed for skip turn.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        private bool TryGetSkipButton(out Button button)
        {
            return _handLayout.transform.GetChild(0).TryGetComponent(out button);
        }
    }
}