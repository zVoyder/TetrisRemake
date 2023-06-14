namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{
    using System;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Features.TurnBasedGDR.CombatSystem.Unit.Managers.Boss;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using VUDK.Features.TurnBasedGDR.SOData;

    [RequireComponent(typeof(BossPhasesManager))]
    public class BossUnitManager : EnemyUnitManager
    {
        [SerializeField]
        private int _buildIndexSceneToLoadOnDefeat;
        [SerializeField]
        private AudioClip _fightClip;
        private BossPhasesManager _bossPhases;

        public event Action OnBossFightInit;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _bossPhases);
        }

        public override void Init(UnitData bossData, Party party, Pool pool)
        {
            base.Init(bossData, party, pool);
            _bossPhases.Init(this, bossData as BossData);
            OnBossFightInit.Invoke();
        }

        /// <summary>
        /// Reinitializes this unit with a new <see cref="UnitData"/>.
        /// </summary>
        /// <param name="bossData">Data of the boss.</param>
        public void ReInit(UnitData bossData)
        {
            base.Init(bossData, Party, RelatedPool);
        }

        public override void Dispose()
        {
            UnitStatusEffects.ExitRemoveAllStatusEffects();

            if(_bossPhases.HasNoMorePhases)
            {
                base.Dispose();
                SceneManager.LoadScene(_buildIndexSceneToLoadOnDefeat, LoadSceneMode.Single);
                return;
            }

            _bossPhases.NextPhase();
        }
    }
}