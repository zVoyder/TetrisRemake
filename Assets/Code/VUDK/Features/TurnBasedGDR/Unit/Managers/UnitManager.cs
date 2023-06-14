namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{
    using System;
    using System.Collections;
    using UnityEngine;
    using VUDK.Patterns.ObjectPool;
    using VUDK.Patterns.ObjectPool.Interfaces;
    using VUDK.Features.TurnBasedGDR.CombatSystem.StateMachines;
    using VUDK.Features.TurnBasedGDR.PartySystem;
    using VUDK.Features.TurnBasedGDR.SOData;

    [RequireComponent(typeof(Unit))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof (UnitAnimatorController))]
    [RequireComponent(typeof(UnitStatusEffects))]
    [RequireComponent(typeof(UnitAudioHandler))]
    public abstract class UnitManager : MonoBehaviour, IPooledObject
    {
        public event Action OnCriticalReceived;
        public event Action OnInitialize;
        public event Action OnUnitTurnStart;
        public event Action OnUnitTurnEnd;

        [SerializeField, Min(0)]
        private float _waitBeforeDisposeTime = 1.5f;
        private SpriteRenderer _spriteRenderer; // This is not really needed because the animator controller override will override the sprite due the its animations

        public Unit Unit { get; private set; }
        public UnitData UnitData { get; private set; }
        public UnitTurns UnitTurns { get; private set; }
        public UnitStatusEffects UnitStatusEffects { get; private set; }
        public UnitAnimatorController UnitAnimatorController { get; private set; }
        public UnitAudioHandler UnitAudioHandler { get; private set; }
        public Pool RelatedPool { get; private set; }
        public Party Party { get; private set; }

        public event Action OnBeforeDeath;

        protected virtual void Awake()
        {
            TryGetComponent(out _spriteRenderer);
            TryGetComponent(out UnitAnimatorController unitAnimatorController);
            TryGetComponent(out Unit unit);
            TryGetComponent(out UnitTurns turns);
            TryGetComponent(out UnitStatusEffects unitStatusEffects);
            TryGetComponent(out UnitAudioHandler unitAudioHandler);

            UnitAudioHandler = unitAudioHandler;
            UnitAnimatorController = unitAnimatorController;
            Unit = unit;
            UnitTurns = turns;
            UnitStatusEffects = unitStatusEffects;
        }

        /// <summary>
        /// Initializes this <see cref="UnitManager"/>.
        /// </summary>
        /// <param name="unitData"><see cref="UnitData"/> of the unit.</param>
        /// <param name="party"><see cref="Party"/> of the unit.</param>
        /// <param name="pool">Related <see cref="Pool"/>.</param>
        public virtual void Init(UnitData unitData, Party party, Pool pool)
        {
            Party = party;
            UnitData = unitData;
            _spriteRenderer.sprite = unitData.UnitSprite;
            AssociatePool(pool);
            Unit.Init(unitData.HitPoints, () => Dispose(), this);
            UnitAudioHandler.Init(unitData.GetHitClip);
            OnInitialize?.Invoke();
        }

        /// <summary>
        /// Starts the unit's turn.
        /// </summary>
        public void UnitTurnStart()
        {
            OnUnitTurnStart?.Invoke();
        }

        /// <summary>
        /// Ends the unit's turn.
        /// </summary>
        public void UnitTurnEnd()
        {
            OnUnitTurnEnd?.Invoke();
        }

        /// <summary>
        /// Receives a critical damage.
        /// </summary>
        public void ReceiveCritical()
        {
            OnCriticalReceived?.Invoke();
        }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public virtual void Dispose()
        {
            OnBeforeDeath?.Invoke();
            UnitStatusEffects.ExitRemoveAllStatusEffects();
            Party.GetComposedUnits().Remove(this);
            StartCoroutine(WaitDisposeRoutine());
        }

        private IEnumerator WaitDisposeRoutine()
        {
            yield return new WaitForSeconds(_waitBeforeDisposeTime);
            RelatedPool.Dispose(gameObject);
        }
    }
}