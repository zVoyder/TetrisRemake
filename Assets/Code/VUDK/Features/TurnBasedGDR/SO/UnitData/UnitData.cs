namespace VUDK.Features.TurnBasedGDR.SOData
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Combat/Units/Generic Unit")]
    public class UnitData : ScriptableObject
    {
        [Header("Info")]
        public string UnitName;

        [Header("Statistics")]
        public int HitPoints;

        [Header("Skill Set")]
        public List<SkillData> Skills;

        [Header("Audio")]
        public AudioClip GetHitClip;

        [Header("Animator")]
        public AnimatorOverrideController AnimatorOverrideControllerGetHit;

        [Header("Graphic")]
        public Sprite UnitSprite;
    }
}