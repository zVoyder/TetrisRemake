namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{ 
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.TurnBasedGDR.CombatSystem.StatusEffects;

    [RequireComponent(typeof(UnitManager))]
    public class UnitStatusEffects : MonoBehaviour
    {
        public event Action<StatusEffectBase> OnAddedEffect;
        public event Action<StatusEffectBase> OnRemovedEffect;

        private UnitManager _unitManager;
        private List<StatusEffectBase> _appliedStatusEffects;

        public int AccumulatedDamage { get; private set; }
        public int CurrentDamageReduction { get; private set; }
        public int CurrentDamageDown { get; private set; }
        public int CurrentDamageUp { get; private set; }
        public bool IsStunned { get; private set; }
        public bool HasDamageReceiveReduction { get; private set; }
        public bool HasDamageDown { get; private set; }
        public bool HasDamageUp { get; private set; }

        private void Awake()
        {
            _appliedStatusEffects = new List<StatusEffectBase>();
            TryGetComponent(out _unitManager);
        }

        private void OnEnable()
        {
            _unitManager.OnInitialize += ResetEffects;
            _unitManager.OnUnitTurnStart += ProcessStatusEffects;
        }

        private void OnDisable()
        {
            _unitManager.OnInitialize -= ResetEffects;
            _unitManager.OnUnitTurnStart -= ProcessStatusEffects;
        }

        /// <summary>
        /// Adds a status effect.
        /// </summary>
        /// <param name="effect"><see cref="StatusEffectBase"/> to add.</param>
        public void AddStatusEffect(StatusEffectBase effect)
        {
            _appliedStatusEffects.Add(effect);
            effect.Enter();
            OnAddedEffect?.Invoke(effect);
        }

        /// <summary>
        /// Processes all the status effects.
        /// </summary>
        private void ProcessStatusEffects()
        {
            for (int i = 0; i < _appliedStatusEffects.Count; i++) // I could not use a foreach loop 'cause the list in a foreach cannot be modiefied in loop
            {
                _appliedStatusEffects[i].Process();

                if (_appliedStatusEffects[i].AppliedTurns <= 0) // also "<" to avoid bugs
                {
                    RemoveStatusEffect(_appliedStatusEffects[i]);
                }
            }

            if(AccumulatedDamage > 0)
                ReleaseAccumulatedDamage();
        }

        /// <summary>
        /// Removes and exit from all status effects.
        /// </summary>
        public void ExitRemoveAllStatusEffects()
        {
            for (int i = 0; i < _appliedStatusEffects.Count; i++)
            {
                RemoveStatusEffect(_appliedStatusEffects[i]);
            }
            _appliedStatusEffects.Clear();
        }

        /// <summary>
        /// Applies the damage reduction modifier.
        /// </summary>
        /// <param name="damageReduction">Damage reduction to apply.</param>
        public void ApplyDamageReduction(int damageReduction)
        {
            CurrentDamageReduction = damageReduction;
            HasDamageReceiveReduction = true;
        }

        /// <summary>
        /// Removes and sets to zero the <see cref="CurrentDamageReduction"/>.
        /// </summary>
        public void RemoveDamageReduction()
        {
            CurrentDamageReduction = 0;
            HasDamageReceiveReduction = false;
        }

        /// <summary>
        /// Applies the damage down.
        /// </summary>
        /// <param name="damageDown">Damage down to apply.</param>
        public void ApplyDamageDown(int damageDown)
        {
            CurrentDamageDown = damageDown;
            HasDamageDown = true;
        }

        /// <summary>
        /// Applies the damage up.
        /// </summary>
        /// <param name="damageDown">Damage up to apply.</param>
        public void ApplyDamageUp(int damageUp)
        {
            CurrentDamageUp = damageUp;
            HasDamageUp = true;
        }

        /// <summary>
        /// Removes and sets to zero the <see cref="CurrentDamageUp"/>.
        /// </summary>
        public void RemoveDamageUp()
        {
            CurrentDamageUp = 0;
            HasDamageUp = false;
        }

        /// <summary>
        /// Removes and sets to zero the <see cref="CurrentDamageDown"/>.
        /// </summary>
        public void RemoveDamageDown()
        {
            CurrentDamageDown = 0;
            HasDamageDown = false;
        }

        /// <summary>
        /// Applies the bleed damage.
        /// </summary>
        /// <param name="bleedDamage">Bleed damage to apply.</param>
        public void ApplyBleedDamage(int bleedDamage)
        {
            AccumulatedDamage += bleedDamage;
        }

        /// <summary>
        /// Applies the stun.
        /// </summary>
        public void ApplyStun()
        {
            IsStunned = true;
        }

        /// <summary>
        /// Removes the stun.
        /// </summary>
        public void RemoveStun()
        {
            IsStunned = false;
        }

        /// <summary>
        /// Applies the Fear.
        /// </summary>
        /// <param name="damageReduction">Damage reduction to apply.</param>
        public void ApplyFear(int damageReduction)
        {
            ApplyStun();
            ApplyDamageReduction(damageReduction);
        }

        /// <summary>
        /// Removes fear.
        /// </summary>
        public void RemoveFear()
        {
            RemoveStun();
            RemoveDamageReduction();
        }

        /// <summary>
        /// Removes a <see cref="StatusEffectBase"/>.
        /// </summary>
        /// <param name="effect"><see cref="StatusEffectBase"/> to remove.</param>
        private void RemoveStatusEffect(StatusEffectBase effect)
        {
            OnRemovedEffect?.Invoke(effect);
            effect.Exit();
            _appliedStatusEffects.Remove(effect);
        }

        /// <summary>
        /// Releases all the accumulated damage.
        /// </summary>
        private void ReleaseAccumulatedDamage()
        {
            _unitManager.Unit.TakeDamage(AccumulatedDamage);
            AccumulatedDamage = 0;
        }


        /// <summary>
        /// Resets the current modifiers.
        /// </summary>
        private void ResetEffects()
        {
            AccumulatedDamage = 0;
            CurrentDamageReduction = 0;
            CurrentDamageDown = 0;
            CurrentDamageUp = 0;
            IsStunned = false;
            HasDamageReceiveReduction = false;
        }
    }
}