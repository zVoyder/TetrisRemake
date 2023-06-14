namespace VUDK.Features.TurnBasedGDR.CombatSystem.PartyComposers
{
    using VUDK.Features.TurnBasedGDR.CombatSystem.Pools;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using VUDK.Features.TurnBasedGDR.SOData;
    using UnityEngine;

    public class EnemyPartyBattleComposer : PartyBattleComposer<UnitManager>
    {
        [SerializeField]
        protected UnitsPool BossPool;

        protected override GameObject PoolUnit(UnitData unitData)
        {
            if (unitData is BossData)
                return BossPool.Get();
            return base.PoolUnit(unitData);
        }
    }
}