namespace VUDK.Features.TurnBasedGDR.SOData
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Combat/Skills/Character Card Skill")]
    public class CardSkillData : SkillData
    {
        public int RechargeTime;

        [Header("Graphic")]
        public Sprite CardFrameSprite;
        public Sprite SkillSprite;

        [Header("Info")]
        public string CardName;
        [TextArea(3, 10)]
        public string Description;
    }
}