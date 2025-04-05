using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyData")]
    public sealed class EnemyData : ScriptableObject
    {
        [Header("MoveComponent")]
        public float speed;

        [Header("TeamComponent")]
        public Team combatTeam;

        [Header("HealthComponent")]
        public int health;

        [Header("WeaponComponent")]
        public BulletData bulletData;
        public float rechargeCooldown;
    }
}