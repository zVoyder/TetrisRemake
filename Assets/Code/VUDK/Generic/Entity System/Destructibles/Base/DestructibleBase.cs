namespace VUDK.Generic.EntitySystem.Destructibles
{
    using VUDK.Generic.EntitySystem.Interfaces;
    using UnityEngine;

    public class DestructibleBase : MonoBehaviour, IVulnerable
    {
        public float HitPoints { get; set; } = 1;

        public virtual void TakeDamage(float hitDamage = 1)
        {
            Death();
        }

        public virtual void Death() 
        {
            HitPoints = 0;
            Destroy(gameObject);
        }
    }
}