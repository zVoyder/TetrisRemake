namespace VUDK.Features.TurnBasedGDR.SOData
{
    using UnityEngine;

    public abstract class StatusEffectData : ScriptableObject
    {
        public Sprite EffectIcon;
        public int AppliedTurns;
    }
}
