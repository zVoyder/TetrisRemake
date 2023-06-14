namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units
{
    using UnityEngine;
    using TurnBasedGDR.SOData;

    [RequireComponent(typeof(Animator))]
    [RequireComponent (typeof(UnitManager))]
    public class UnitAnimatorController : MonoBehaviour
    {
        private const string GetHitTrigger = "GetHit";
        private const string AttackTrigger = "Attack";

        private Animator _anim;
        private UnitManager _unitManager;

        private void Awake()
        {
            TryGetComponent(out _anim);
            TryGetComponent(out _unitManager);
        }

        private void OnEnable()
        {
            _unitManager.OnInitialize += () => OverrideAnimator(_unitManager.UnitData.AnimatorOverrideControllerGetHit);
        }

        private void OnDisable()
        {
            _unitManager.OnInitialize -= () => OverrideAnimator(_unitManager.UnitData.AnimatorOverrideControllerGetHit);
        }

        /// <summary>
        /// Animates the Unit <see cref="Animator"/> with a <see cref="SkillData.SkillAnimatorOverrideController"/>.
        /// </summary>
        /// <param name="skill"><see cref="SkillData"/> to use.</param>
        public void AnimSkill(SkillData skill)
        {
            OverrideAnimator(skill.SkillAnimatorOverrideController);
            _anim.SetTrigger(AttackTrigger);
        }

        /// <summary>
        /// Animates the Unit <see cref="Animator"/> with the <see cref="UnitData.AnimatorOverrideControllerGetHit"/>.
        /// </summary>
        public void AnimGetHit()
        {
            OverrideAnimator(_unitManager.UnitData.AnimatorOverrideControllerGetHit);
            _anim.SetTrigger(GetHitTrigger);
        }

        /// <summary>
        /// Overrides this <see cref="Animator"/> with an <see cref="AnimatorOverrideController"/>.
        /// </summary>
        /// <param name="aoc"><see cref="AnimatorOverrideController"/> to use for overriding this <see cref="Animator"/></param>
        private void OverrideAnimator(AnimatorOverrideController aoc)
        {
            _anim.runtimeAnimatorController = aoc;
        }
    }
}