namespace VUDK.Features.TurnBasedGDR.SOData
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Item/Card")]
    public class CardItemData : ItemBaseData
    {
        public CardSkillData CardData;
        public UnitData UnitDataOwner;
    }
}