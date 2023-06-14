
namespace VUDK.Generic.EntitySystem.Interfaces
{
    interface IEntityVulnerable : IVulnerable
    {
        void HealHitPoints(float healPoints);
    }
}