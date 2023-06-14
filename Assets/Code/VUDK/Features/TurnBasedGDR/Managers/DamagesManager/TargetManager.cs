namespace VUDK.Features.TurnBasedGDR.CombatSystem.Managers
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using VUDK.Features.TurnBasedGDR.SOData.Structures.Enums;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units.CardsSystem;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.SOData;

    public class TargetManager : MonoBehaviour
    {
        private UnitCard _selectedCard;
        private Unit _selectedTargetUnit;

        [field: SerializeField]
        public AttacksManager AttacksManager { get; private set; }

        public event Action<UnitCard> OnCheckValidTargets;
        public event Action OnTargetAcquisitionCompleted;

        private void Update()
        {
            if (_selectedCard)
                CardSelecting();
        }

        /// <summary>
        /// Sets the card of this <see cref="TargetManager"/> with a <see cref="UnitCard"/>.
        /// </summary>
        /// <param name="selectedCard"><see cref="UnitCard"/> to select.</param>
        public void SelectCard(UnitCard selectedCard)
        {
            OnTargetAcquisitionCompleted?.Invoke();

            if(_selectedCard && _selectedCard != selectedCard)
                _selectedCard.SetSelectAnimation(false);

            _selectedCard = selectedCard;
        }

        /// <summary>
        /// Checks if the targeted <see cref="UnitManager"/> is a valid target.
        /// </summary>
        /// <param name="unitAttacker">Unit Attacker.</param>
        /// <param name="skillData">Skill to use.</param>
        /// <param name="selectedTargetUnit">Targeted unit.</param>
        /// <returns>True if is valid, False if not.</returns>
        public bool IsValidTargetUnit(UnitManager unitAttacker, SkillData skillData, UnitManager selectedTargetUnit)
        {
            if (skillData.SkillTarget.HasFlag(SkillTarget.Everything))
            {
                return true;
            }

            if (skillData.SkillTarget.HasFlag(SkillTarget.Self))
            {
                return unitAttacker == selectedTargetUnit;
            }

            if (skillData.SkillTarget.HasFlag(SkillTarget.SameParty))
            {
                return IsSameParty(unitAttacker, selectedTargetUnit);
            }

            if (skillData.SkillTarget.HasFlag(SkillTarget.OpponentParty))
            {
                return !IsSameParty(unitAttacker, selectedTargetUnit);
            }

            return false;
        }

        /// <summary>
        /// Card selecting with mouse input.
        /// </summary>
        private void CardSelecting()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) // There is no need to check what is clicking in the 3d world anymore 
                {
                    OnCheckValidTargets?.Invoke(_selectedCard);    // because the UnitCard knows when the pointer is clicking on it
                    return;                                        // Otherwise it will instantly deselect the card 'cause the raycast won't hit a UI gameobject
                }

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                {
                    if (_selectedCard && hit.transform.TryGetComponent(out UnitManager targetedUnit))
                    {
                        TargetAttackUnit(targetedUnit);
                    }
                }

                SelectCard(null);
            }
        }

        /// <summary>
        /// Sends an attack to the targeted unit.
        /// </summary>
        /// <param name="targetedUnit">Targeted unit.</param>
        private void TargetAttackUnit(UnitManager targetedUnit)
        {
            if (IsValidTargetUnit(_selectedCard.RelatedHand.RelatedUnitManager, _selectedCard.Data, targetedUnit))
            {
                UnitManager unitAttacker = _selectedCard.RelatedHand.RelatedUnitManager;
                AttacksManager.UseSkillOnUnit(unitAttacker, _selectedCard.Data, targetedUnit);
                _selectedCard.UseCard();
                unitAttacker.UnitTurns.NextState();
            }
        }

        /// <summary>
        /// Checks if a <see cref="UnitManager"/> is the same party of an other <see cref="UnitManager"/>.
        /// </summary>
        /// <param name="unitManager">First unit.</param>
        /// <param name="targetUnit">Second unit.</param>
        /// <returns>True if they are of the same party, False if not.</returns>
        private bool IsSameParty(UnitManager unitManager, UnitManager targetUnit)
        {
            return unitManager.Party == targetUnit.Party;
        }
    }
}