namespace VUDK.Features.TurnBasedGDR.CombatSystem.Managers
{
    using UnityEngine;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Features.TurnBasedGDR.SOData.Structures.Enums;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units.CardsSystem;

    public class UICombatManager : MonoBehaviour
    {
        [field: SerializeField, Header("Pool")]
        public Pool PoolIconEffect { get; private set; }

        [SerializeField] private TargetManager _targetManager;
        [SerializeField] private GameObject _indicatorPlayerParty, _indicatorEnemyParty;

        private void OnEnable()
        {
            _targetManager.OnCheckValidTargets += ShowIndicatorValidTargets;
            _targetManager.OnTargetAcquisitionCompleted += DisableIndicators;
        }

        private void OnDisable()
        {
            _targetManager.OnCheckValidTargets -= ShowIndicatorValidTargets;
        }

        /// <summary>
        /// Shows the indicator of the correct card targets.
        /// </summary>
        /// <param name="card"></param>
        private void ShowIndicatorValidTargets(UnitCard card)
        {
            // That's simply logic cause only the player when selecting the card
            // will show the indicators and the SameParty value for the UnitCard cards means the PlayerParty
            _indicatorPlayerParty.SetActive(card.Data.SkillTarget.HasFlag(SkillTarget.SameParty));
            _indicatorEnemyParty.SetActive(card.Data.SkillTarget.HasFlag(SkillTarget.OpponentParty));
        }

        /// <summary>
        /// Disables the indicators.
        /// </summary>
        private void DisableIndicators()
        {
            _indicatorPlayerParty.SetActive(false);
            _indicatorEnemyParty.SetActive(false);
        }
    }
}