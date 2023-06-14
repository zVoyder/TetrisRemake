namespace VUDK.Features.TurnBasedGDR.SOData.Structures.Enums
{
    public enum SkillType : uint
    {
        Damage,
        Heal
    }

    [System.Flags]
    public enum SkillTarget : uint
    {
        None = 0,
        SameParty = 1,
        OpponentParty = 2,
        Self = 4,
        Everything
    }

    public enum ItemType
    {
        Base,
        Card,
        Bandage
    }
}