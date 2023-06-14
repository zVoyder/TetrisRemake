namespace VUDK.Features.TurnBasedGDR.CombatSystem.PartyComposers
{
    using UnityEngine;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Pools;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public abstract class PartyBattleComposer<T> : MonoBehaviour where T : UnitManager
    {
        [SerializeField, Header("Setup")]
        private Transform _parent;
        [SerializeField]
        private Vector3 _spaceBetweenEachOther;

        [field: SerializeField, Header("Manager")]
        public InFightUnitsManager InFightUnitsManager { get; protected set; }

        [SerializeField, Header("Pools")]
        protected UnitsPool Pool;

        /// <summary>
        /// Composes the units of a <see cref="Party"/> by getting them from the <see cref="UnitsPool"/>.
        /// </summary>
        /// <param name="party"><see cref="Party"/> to compose.</param>
        public void ComposeUnits(Party party)
        {
            Vector3 startingPoint = _parent.position;

            foreach (UnitData unitData in party.UnitsData)
            {
                GameObject pooledUnit = PoolUnit(unitData);

                if (pooledUnit.TryGetComponent(out T unit))
                {
                    GenerateUnit(unit, unitData, party, startingPoint);
                    startingPoint += _spaceBetweenEachOther;
                }
            }
        }

        /// <summary>
        /// Initializes the <see cref="UnitManager"/> and adds it to the <see cref="InFightUnitsManager"/> composed list.
        /// </summary>
        /// <param name="unitManager"><see cref="UnitManager"/> to initialize.</param>
        /// <param name="unitData"><see cref="UnitData"/> to use on the Unit.</param>
        /// <param name="relatedParty">Related <see cref="Party"/></param>
        /// <param name="position">Position to generate the unit in.</param>
        protected virtual void GenerateUnit(T unitManager, UnitData unitData, Party relatedParty, Vector3 position)
        {
            unitManager.Init(unitData, relatedParty, Pool);
            InFightUnitsManager.Add(unitManager);
            SetUnitBattleName(unitManager);
            SetUnitBattlePosition(unitManager, position);
        }

        /// <summary>
        /// Pools a unit from the <see cref="UnitsPool"/>.
        /// </summary>
        /// <param name="unitData"><see cref="UnitData"/> of the pooled unit.</param>
        /// <returns><see cref="GameObject"/> of the pooled unit.</returns>
        protected virtual GameObject PoolUnit(UnitData unitData)
        {
            return Pool.Get();
        }

        /// <summary>
        /// Sets the unit position in battle.
        /// </summary>
        /// <param name="unit">Unit to position.</param>
        /// <param name="position">Position to set the unit in.</param>
        protected void SetUnitBattlePosition(UnitManager unit, Vector3 position)
        {
            unit.transform.SetParent(_parent);
            unit.transform.position = position;
        }

        /// <summary>
        /// Sets the unit name in battle.
        /// </summary>
        /// <param name="unit">Unit.</param>
        protected void SetUnitBattleName(UnitManager unit)
        {
            unit.transform.name = $"Unit {unit.UnitData.UnitName}";
            unit.transform.name = unit.UnitData.UnitName;
        }
    }
}