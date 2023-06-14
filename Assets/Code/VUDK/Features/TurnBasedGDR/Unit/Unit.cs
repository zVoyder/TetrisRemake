namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{
    using System;
    using UnityEngine;
    using VUDK.Generic.EntitySystem;

    public class Unit : EntityBase
    {
        public event Action OnDeath;
        private UnitManager _unitManager;

        /// <summary>
        /// Initializes this Unit <see cref="EntityBase"/>.
        /// </summary>
        /// <param name="hitPoints">Hit points.</param>
        /// <param name="onDeath">Event on death.</param>
        /// <param name="unitManager">Related <see cref="UnitManager"/>.</param>
        public virtual void Init(int hitPoints, Action onDeath, UnitManager unitManager)
        {
            MaxHitPoints = hitPoints;
            startingHitPoints = hitPoints;
            OnDeath = onDeath;
            _unitManager = unitManager;
            SetupHP();
        }

        public override void Death()
        {
            base.Death();
            OnDeath?.Invoke();
        }

        public override void TakeDamage(float hitDamage = 1)
        {
            hitDamage -= hitDamage / 100 * _unitManager.UnitStatusEffects.CurrentDamageReduction;
            base.TakeDamage(Mathf.Round(hitDamage));
        }
    }
}
