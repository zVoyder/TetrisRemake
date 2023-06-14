namespace VUDK.Generic.EntitySystem
{
    using VUDK.Generic.EntitySystem.Interfaces;
    using System;
    using UnityEngine;

    public class EntityBase : MonoBehaviour, IEntityVulnerable
    {
        protected float startingHitPoints;
        public float MaxHitPoints { get; protected set; }
        public float CurrentHitPoints { get; private set; }
        public bool IsAlive { get; private set; } = true;

        /// <summary>
        /// On Change hit points Action event delegate.
        /// <code><see cref="(T1)"/> as The hit points change receiver.</code>
        /// <code><see cref="(T2)"/> as The current hit points.</code>
        /// <code><see cref="(T3)"/> as The maximum hit points.</code>
        /// </summary>
        public event Action<float, float, float> OnChangeHitPoints;

        /// <summary>
        /// On hit points setup Action event delegate.
        /// <code><see cref="(T1)"/> as The current hit points.</code>
        /// <code><see cref="(T2)"/> as The maximum hit points.</code>
        /// </summary>
        public event Action<float, float> OnHitPointsSetUp;

        public event Action OnTakeDamage;
        public event Action OnHealHitPoints;

        protected virtual void SetupHP()
        {
            IsAlive = true;
            CurrentHitPoints = startingHitPoints;

            if (CurrentHitPoints > startingHitPoints)
            {
                startingHitPoints = MaxHitPoints;
                CurrentHitPoints = startingHitPoints;
            }

            OnHitPointsSetUp?.Invoke(CurrentHitPoints, MaxHitPoints);
        }

        public virtual void TakeDamage(float hitDamage = 1f)
        {
            OnTakeDamage?.Invoke();

            CurrentHitPoints -= Mathf.Abs(hitDamage);

            if (CurrentHitPoints <= 0.1f)
            {
                CurrentHitPoints = 0f;
                Death();
            }

            OnChangeHitPoints?.Invoke(hitDamage, CurrentHitPoints, MaxHitPoints);
        }

        public virtual void HealHitPoints(float healPoints)
        {
            OnHealHitPoints?.Invoke();

            IsAlive = true;
            CurrentHitPoints += Mathf.Abs(healPoints);

            if (CurrentHitPoints > MaxHitPoints)
                CurrentHitPoints = MaxHitPoints;

            OnChangeHitPoints?.Invoke(healPoints, CurrentHitPoints, MaxHitPoints);
        }

        public virtual void Death()
        {
            IsAlive = false;
        }
    }
}