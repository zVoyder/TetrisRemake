namespace VUDK.Features.TurnBasedGDR.SOData
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Serializable.Mathematics;
    using VUDK.Features.TurnBasedGDR.SOData.Structures.Enums;

    [CreateAssetMenu(menuName = "Combat/Skills/Generic Skill")]
    public class SkillData : ScriptableObject
    {
        [Header("Statistics")]
        public Range<int> Power;
        [Range(0, 100)] public byte CriticalChance;

        [Header("Animation")]
        public AnimatorOverrideController SkillAnimatorOverrideController;

        [Header("Audio")]
        public AudioClip AttackClip;

        [Header("Effects")]
        public List<StatusEffectData> SkillStatusEffects;

        [Header("Settings")]
        public SkillType SkillType;
        public SkillTarget SkillTarget;
        public bool IsAreaOfEffect;
    }
}