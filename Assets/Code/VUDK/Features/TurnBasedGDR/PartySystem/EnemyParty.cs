namespace VUDK.Features.TurnBasedGDR.PartySystem
{
    using VUDK.Features.TurnBasedGDR.CombatSystem.Managers;

    public class EnemyParty : Party
    {
        private void Start()
        {
            AssociateInFightUnitsManager(CombatManager.Instance.EnemyPartyComposer.InFightUnitsManager);
        }
    }
}