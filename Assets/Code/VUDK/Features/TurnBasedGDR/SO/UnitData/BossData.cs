namespace VUDK.Features.TurnBasedGDR.SOData
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Combat/Units/Boss")]
    public class BossData : UnitData
    {
        [Header("Boss Phases")]
        public List<UnitData> Phases;
    }
}