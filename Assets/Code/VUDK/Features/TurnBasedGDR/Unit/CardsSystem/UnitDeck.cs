namespace VUDK.Features.TurnBasedGDR.CombatSystem.Units.CardsSystem
{
    using VUDK.Features.TurnBasedGDR.SOData;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class UnitDeck : MonoBehaviour
    {
        public List<SkillData> CardDatas { get; private set; }

        /// <summary>
        /// Initializes the list of <see cref="SkillData"/>.
        /// </summary>
        /// <param name="_defaultCardDatas"></param>
        public void Init (List<SkillData> _defaultCardDatas)
        {
            CardDatas = _defaultCardDatas.ToList(); // ToList() creates a new List with the same elements
        }
    }
}