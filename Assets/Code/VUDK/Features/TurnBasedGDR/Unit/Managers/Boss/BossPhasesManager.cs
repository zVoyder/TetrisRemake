namespace VUDK.Features.TurnBasedGDR.CombatSystem.Unit.Managers.Boss
{
    using UnityEngine;
    using VUDK.Features.TurnBasedGDR.SOData;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Units;

    public class BossPhasesManager : MonoBehaviour
    {
        public int PhasesCount => _data.Phases.Count;
        public bool HasNoMorePhases => PhasesCount <= CurrentPhase;

        private BossData _data;
        private BossUnitManager _bossManager;
        public int CurrentPhase { get; private set; }

        /// <summary>
        /// Initializes this <see cref="BossPhasesManager"/>.
        /// </summary>
        /// <param name="bossManager"><see cref="BossUnitManager"/> unit.</param>
        /// <param name="data">Data of the boss.</param>
        public void Init(BossUnitManager bossManager, BossData data)
        {
            _data = data;
            _bossManager = bossManager;
        }

        /// <summary>
        /// Goes to the next phase of this <see cref="BossPhasesManager"/>.
        /// </summary>
        public void NextPhase()
        {
            if (HasNoMorePhases) return;

            _bossManager.ReInit(_data.Phases[CurrentPhase]);
            CurrentPhase++;
        }
    }
}