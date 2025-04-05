using UnityEngine;

namespace ShootEmUp
{
    internal static class BulletUtils
    {
        internal static void DealDamage(Bullet bullet, IDamageTaker other)
        {
            if (bullet.TeamComponent.CombatTeam == other.TeamComponent.CombatTeam)
            {
                return;
            }
            other.TakeDamage(bullet.damage);
        }
    }
}