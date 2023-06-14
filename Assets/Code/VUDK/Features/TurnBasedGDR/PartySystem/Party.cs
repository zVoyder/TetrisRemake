namespace VUDK.Features.TurnBasedGDR.PartySystem
{
    using UnityEngine;
    using System.Collections.Generic;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public abstract class Party : MonoBehaviour
    {
        private InFightUnitsManager _relatedInFightUnitManager;

        [field: SerializeField]
        public List<UnitData> UnitsData { get; private set; }

        /// Builds a new party using a list of <see cref="UnitData"/>.
        /// </summary>
        /// <param name="units">List of <see cref="UnitData"/> use for building the party.</param>
        public void BuildParty(List<UnitData> units)
        {
            UnitsData = units;
        }

        /// <summary>
        /// Associates an <see cref="InFightUnitsManager"/> to this <see cref="Party"/>.
        /// </summary>
        /// <param name="inFightUnitsManager"><see cref="InFightUnitsManager"/> to associate.</param>
        public void AssociateInFightUnitsManager(InFightUnitsManager inFightUnitsManager)
        {
            _relatedInFightUnitManager = inFightUnitsManager;
        }

        /// <summary>
        /// Gets a list of <see cref="UnitManager"/> of the composed units of <see cref="_relatedInFightUnitManager"/>.
        /// </summary>
        /// <returns>List of composed in fight <see cref="UnitManager"/>.</returns>
        public List<UnitManager> GetComposedUnits()
        {
            return _relatedInFightUnitManager.GetComposedUnits();
        }

        /// <summary>
        /// Checks if there are members alive.
        /// </summary>
        /// <returns>True if there are, False if not.</returns>
        public bool AreThereMembersAlive()
        {
            return _relatedInFightUnitManager.GetComposedUnits().Count > 0;
        }
    }
}