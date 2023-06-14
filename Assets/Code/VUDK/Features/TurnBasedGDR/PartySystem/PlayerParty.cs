namespace VUDK.Features.TurnBasedGDR.PartySystem
{
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public class PlayerParty : Party
    {
        private void Start()
        {
            AssociateInFightUnitsManager(CombatManager.Instance.PlayerPartyComposer.InFightUnitsManager);
        }
    }
}