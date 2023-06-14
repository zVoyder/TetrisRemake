namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{
    using System.Linq;
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    [RequireComponent(typeof(EnemyUnitManager))]
    public class EnemyUnitAI : MonoBehaviour
    {
        private EnemyUnitManager _enemyUnitManager;

        private void Awake()
        {
            TryGetComponent(out _enemyUnitManager);
        }

        /// <summary>
        /// Attacks a random <see cref="UnitManager"/> target.
        /// </summary>
        public void AttackRandomTarget()
        {
            SkillData skill = GetRandomSkill();

            if(TryGetValidTarget(skill, out UnitManager targetedUnit))
            {
                CombatManager.Instance.AttacksManager.UseSkillOnUnit(_enemyUnitManager, skill, targetedUnit);
                return;
            }
        }

        /// <summary>
        /// Tries to get a valid <see cref="UnitManager"/> target.
        /// </summary>
        /// <param name="skill"><see cref="SkillData"/> to use.</param>
        /// <param name="targetedUnit">Targeted <see cref="UnitManager"/>.</param>
        /// <returns>True if it is valid, False if not.</returns>
        private bool TryGetValidTarget(SkillData skill, out UnitManager targetedUnit)
        {
            List<UnitManager> playerUnits = CombatManager.Instance.PlayerPartyComposer.InFightUnitsManager.GetComposedUnits();
            List<UnitManager> enemyUnits = CombatManager.Instance.EnemyPartyComposer.InFightUnitsManager.GetComposedUnits();

            List<UnitManager> targetsList = playerUnits.Concat(enemyUnits).ToList();

            List<UnitManager> possibleTargets = GetPossibleTargets(skill, targetsList);

            if(possibleTargets.Count <= 0)
            {
                targetedUnit = null;
                return false;
            }

            targetedUnit = possibleTargets[Random.Range(0, possibleTargets.Count)];
            return true;
        }

        /// <summary>
        /// Gets all possible targets of a <see cref="SkillData"/> from a list of <see cref="UnitManager"/>.
        /// </summary>
        /// <param name="skill"><see cref="SkillData"/> to use.</param>
        /// <param name="allUnits">List of all <see cref="UnitManager"/> possible targets.</param>
        /// <returns>A <see cref="UnitManager"/> list of all possible targets.</returns>
        private List<UnitManager> GetPossibleTargets(SkillData skill, List<UnitManager> allUnits)
        {
            List<UnitManager> possibleTargets = new List<UnitManager>();
            
            foreach(UnitManager unitPossibleTarget in allUnits)
            {
                if(CombatManager.Instance.TargetManager.IsValidTargetUnit(_enemyUnitManager, skill, unitPossibleTarget))
                {
                    possibleTargets.Add(unitPossibleTarget);
                    //Debug.Log("Possible Target found " + unitPossibleTarget.UnitData.UnitName);
                }
            }

            return possibleTargets;
        }

        /// <summary>
        /// Gets a random <see cref="SkillData"/>.
        /// </summary>
        /// <returns>A random <see cref="SkillData"/>.</returns>
        private SkillData GetRandomSkill()
        {
            return _enemyUnitManager.UnitData.Skills[Random.Range(0, _enemyUnitManager.UnitData.Skills.Count)];
        }
    }
}