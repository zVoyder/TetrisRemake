namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    [RequireComponent(typeof(UnitManager), typeof(UnitStatusEffects))]
    public class UnitUI : MonoBehaviour
    {
        private const string ReceivedDamageAnimatorTriggerParamer = "Play";

        [SerializeField, Header("Unit Turn Indicator")]
        private GameObject _indicator;

        [SerializeField, Header("Status Effects")]
        private GridLayoutGroup _effectsLayoutGroup;

        [SerializeField, Header("Images")]
        private Image _redBarImage;
        [SerializeField]
        private Image _deathSymbol;

        [SerializeField, Header("Texts")]
        private TMP_Text _hitPointsText;
        [SerializeField]
        private TMP_Text _receivedDamageText;

        [SerializeField, Header("Text Colors")]
        private Color _receivedCriticalColor = Color.yellow;
        [SerializeField]
        private Color _receivedDamageTextColor = Color.red;
        [SerializeField]
        private Color _receivedHealTextColor = Color.green;

        [SerializeField, Header("Animators")]
        private Animator _animReceivedDamage;

        private UnitManager _unitManager;
        private Dictionary<StatusEffectBase, Image> _dictStatusEffectImage;
        private Pool _iconStatusEffectPool;

        private void Awake()
        {
            _iconStatusEffectPool = CombatManager.Instance.UICombatManager.PoolIconEffect;/*.Get(_effectsLayoutGroup.transform)*/
            TryGetComponent(out _unitManager);
        }

        private void OnEnable()
        {
            _dictStatusEffectImage = new Dictionary<StatusEffectBase, Image>();

            _unitManager.OnInitialize += RemoveAllEffectIcons;
            _unitManager.OnInitialize += () => SetActiveDeathSymbol(false);
            _unitManager.OnBeforeDeath += () => SetActiveDeathSymbol(true);
            _unitManager.Unit.OnHitPointsSetUp += SetHitPoints;
            _unitManager.Unit.OnTakeDamage += SetColorDamage;
            _unitManager.Unit.OnHealHitPoints += SetColorHeal;
            _unitManager.OnCriticalReceived += SetColorCritical;
            _unitManager.Unit.OnChangeHitPoints += UpdateHitPointsUI;
            _unitManager.Unit.OnChangeHitPoints += HitPointsChangeAnimation;

            _unitManager.OnUnitTurnStart += () => _indicator.SetActive(true);
            _unitManager.OnUnitTurnEnd += () => _indicator.SetActive(false);
            _unitManager.UnitStatusEffects.OnAddedEffect += AddEffectIcon;
            _unitManager.UnitStatusEffects.OnRemovedEffect += RemoveEffect;
        }


        private void OnDisable()
        {
            _unitManager.OnInitialize -= RemoveAllEffectIcons;
            _unitManager.OnInitialize -= () => SetActiveDeathSymbol(false);
            _unitManager.OnBeforeDeath -= () => SetActiveDeathSymbol(true);
            _unitManager.Unit.OnHitPointsSetUp -= SetHitPoints;
            _unitManager.Unit.OnTakeDamage -= SetColorDamage;
            _unitManager.Unit.OnHealHitPoints -= SetColorHeal;
            _unitManager.OnCriticalReceived -= SetColorCritical;
            _unitManager.Unit.OnChangeHitPoints -= UpdateHitPointsUI;
            _unitManager.Unit.OnChangeHitPoints -= HitPointsChangeAnimation;
            _unitManager.UnitStatusEffects.OnAddedEffect -= AddEffectIcon;

            _unitManager.OnUnitTurnStart -= () => _indicator.SetActive(true);
            _unitManager.OnUnitTurnEnd -= () => _indicator.SetActive(false);
            _unitManager.UnitStatusEffects.OnAddedEffect -= AddEffectIcon;
            _unitManager.UnitStatusEffects.OnRemovedEffect -= RemoveEffect;
        }

        /// <summary>
        /// Sets the <see cref="_hitPointsText"/>.
        /// </summary>
        /// <param name="currentHitPoints">Current hit points.</param>
        /// <param name="maxHitPoints">Max hit points.</param>
        private void SetHitPoints(float currentHitPoints, float maxHitPoints)
        {
            ChangeHitPoints(currentHitPoints, maxHitPoints);
        }

        /// <summary>
        /// Updates the <see cref="_hitPointsText"/>.
        /// </summary>
        /// <param name="hitPointsChange">Hit points difference between last hitpoints and the new current hit points.</param>
        /// <param name="currentHitPoints">Current hit points.</param>
        /// <param name="maxHitPoints">Max hit points.</param>
        private void UpdateHitPointsUI(float hitPointsChange, float currentHitPoints, float maxHitPoints)
        {
            ChangeHitPoints(currentHitPoints, maxHitPoints);
        }

        /// <summary>
        /// Sets the <see cref="_receivedDamageText"/> color's text with <see cref="_receivedCriticalColor"/>.
        /// </summary>
        private void SetColorCritical()
        {
            _receivedDamageText.color = _receivedCriticalColor;
        }

        /// <summary>
        /// Sets the <see cref="_receivedDamageText"/> color's text with <see cref="_receivedDamageTextColor"/>.
        /// </summary>
        private void SetColorDamage()
        {
            _receivedDamageText.color = _receivedDamageTextColor;
        }

        /// <summary>
        /// Sets the <see cref="_receivedDamageText"/> color's text with <see cref="_receivedHealTextColor"/>.
        /// </summary>
        private void SetColorHeal()
        {
            _receivedDamageText.color = _receivedHealTextColor;
        }

        /// <summary>
        /// Animates the <see cref="_receivedDamageText"/>.
        /// </summary>
        private void HitPointsChangeAnimation(float hitPointsChange, float currentHitPoints, float maxHitPoints)
        {
            _receivedDamageText.text = hitPointsChange.ToString();
            _animReceivedDamage.SetTrigger(ReceivedDamageAnimatorTriggerParamer);
        }

        /// <summary>
        /// Adds the status effect icon image of a <see cref="StatusEffectBase"/>.
        /// </summary>
        /// <param name="effect"><see cref="StatusEffectBase"/> effect.</param>
        private void AddEffectIcon(StatusEffectBase effect)
        {
            GameObject iconGO = _iconStatusEffectPool.Get(_effectsLayoutGroup.transform);

            if (iconGO.TryGetComponent(out Image image))
            {
                image.sprite = effect.Data.EffectIcon;
                _dictStatusEffectImage.Add(effect, image);
            }
        }

        /// <summary>
        /// Removes an effect gameobject icon.
        /// </summary>
        /// <param name="effect"><see cref="StatusEffectBase"/> to remove.</param>
        private void RemoveEffect(StatusEffectBase effect)
        {
            if (_dictStatusEffectImage.ContainsKey(effect))
            {
                RemoveEffectIcon(effect);
                _dictStatusEffectImage.Remove(effect);
            }
        }

        /// <summary>
        /// Removes the status effect icon image of a <see cref="StatusEffectBase"/>.
        /// </summary>
        /// <param name="effect"><see cref="StatusEffectBase"/> effect.</param>
        private void RemoveEffectIcon(StatusEffectBase effect)
        {
            _iconStatusEffectPool.Dispose(_dictStatusEffectImage[effect].gameObject);
        }

        /// <summary>
        /// Changes the hit points text.
        /// </summary>
        /// <param name="currentHitPoints">Current hit points.</param>
        /// <param name="maxHitPoints">Max hit points.</param>
        private void ChangeHitPoints(float currentHitPoints, float maxHitPoints)
        {
            _hitPointsText.text = $"{currentHitPoints} / {maxHitPoints}";
            _redBarImage.fillAmount = currentHitPoints / maxHitPoints;
        }

        /// <summary>
        /// Removes all status effect icons.
        /// </summary>
        private void RemoveAllEffectIcons()
        {
            int childCount = _effectsLayoutGroup.transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                _iconStatusEffectPool.Dispose(_effectsLayoutGroup.transform.GetChild(i).gameObject);
            }
            _dictStatusEffectImage.Clear();
        }

        /// <summary>
        /// Set active the <see cref="_deathSymbol"/>.
        /// </summary>
        /// <param name="enabled">True to enable it, False to disable it.</param>
        private void SetActiveDeathSymbol(bool enabled)
        {
            _deathSymbol.enabled = enabled;
        }
    }
}