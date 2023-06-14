namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units.CardsSystem
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using TMPro;
    using VUDK.Patterns.ObjectPool.Interfaces;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    [RequireComponent(typeof(Image), typeof(Animator))]
    public class UnitCard : MonoBehaviour, IPointerDownHandler, IPooledObject
    {
        private const string SelectTriggerParameter = "Selected";

        [Header("Images")]
        [SerializeField]
        private Image _cardIconImage;
        [SerializeField]
        private Image _obscureCardImage;

        [Header("Texts")]
        [SerializeField]
        private TMP_Text _cardDescriptionText;
        [SerializeField]
        private TMP_Text _cardNameText;
        [SerializeField]
        private TMP_Text _cardAttackText;
        [SerializeField]
        private TMP_Text _cardCriticalChanceText;
        [SerializeField]
        private TMP_Text _cardFixedTurnsText;
        [SerializeField]
        private TMP_Text _cardTurnsWaitBeforeAvailable;

        private Animator _anim;
        private Image _cardFrameImage;
        private int _turnsToWaitBeforeAvailable;

        public UnitHand RelatedHand { get; private set; }
        public CardSkillData Data { get; private set; }
        public Pool RelatedPool { get; private set; }

        public bool IsCardAvailable => _turnsToWaitBeforeAvailable == 0;

        private void Awake ()
        {
            TryGetComponent(out _anim);
            TryGetComponent(out _cardFrameImage);
        }

        private void Start()
        {
            RelatedHand.RelatedUnitManager.OnUnitTurnStart += OnUnitTurnCard;
        }

        /// <summary>
        /// Initializes the <see cref="UnitCard"/>.
        /// </summary>
        /// <param name="data"><see cref="CardSkillData"/> data.</param>
        /// <param name="relatedHand">related <see cref="UnitHand"/>.</param>
        /// <param name="relatedPool">related <see cref="Pool"/></param>
        public void Init(CardSkillData data, UnitHand relatedHand, Pool relatedPool)
        {
            AssociatePool(relatedPool);
            RelatedHand = relatedHand;
            Data = data;
            transform.name = "Card " + Data.name;
            ResetCardTurns();
            SetUpText(data);
            SetUpImages(data);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(IsCardAvailable)
                SendToTargetManager();
        }

        /// <summary>
        /// Sends this <see cref="UnitCard"/> to the <see cref="TargetManager"/>
        /// </summary>
        public void SendToTargetManager()
        {
            SetSelectAnimation(true);
            CombatManager.Instance.TargetManager.SelectCard(this);
        }

        /// <summary>
        /// Resets the turns with its relative <see cref="UnitData"/>'s recharge time.
        /// </summary>
        public void UseCard()
        {
            _turnsToWaitBeforeAvailable = Data.RechargeTime;
        }

        /// <summary>
        /// Sets the turns of the cards to zero.
        /// </summary>
        private void ResetCardTurns()
        {
            _turnsToWaitBeforeAvailable = 0;
        }

        /// <summary>
        /// Sets active the card's animation.
        /// </summary>
        /// <param name="enabled"></param>
        public void SetSelectAnimation(bool enabled)
        {
            _anim.SetBool(SelectTriggerParameter, enabled);
        }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public void Dispose()
        {
            RelatedPool.Dispose(gameObject);
        }

        private void OnUnitTurnCard()
        {
            if (!IsCardAvailable)
            {
                _turnsToWaitBeforeAvailable--;
                UpdateRemainingTurnsBeforeAvailableText();
            }

            ObscureCard(!IsCardAvailable);
        }

        /// <summary>
        /// Setups the texts.
        /// </summary>
        /// <param name="data"></param>
        private void SetUpText(CardSkillData data)
        {
            _cardDescriptionText.text = data.Description;
            _cardNameText.text = data.CardName;
            _cardAttackText.text = $"{data.Power.Min} ~ {data.Power.Max}";
            _cardCriticalChanceText.text = $"{data.CriticalChance}%";
            _cardFixedTurnsText.text = data.RechargeTime.ToString();
        }

        /// <summary>
        /// Setups the images.
        /// </summary>
        /// <param name="data"></param>
        private void SetUpImages(CardSkillData data)
        {
            _cardIconImage.sprite = Data.SkillSprite;
            _cardFrameImage.sprite = Data.CardFrameSprite;
        }

        /// <summary>
        /// Obscures the card.
        /// </summary>
        /// <param name="enabled"></param>
        private void ObscureCard(bool enabled)
        {
            _obscureCardImage.gameObject.SetActive(enabled);
        }

        /// <summary>
        /// Updates the remaining turns text.
        /// </summary>
        private void UpdateRemainingTurnsBeforeAvailableText()
        {
            _cardTurnsWaitBeforeAvailable.text = _turnsToWaitBeforeAvailable.ToString();
        }
    }
}