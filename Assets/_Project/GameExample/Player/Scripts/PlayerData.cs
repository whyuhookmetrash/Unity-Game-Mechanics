using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
    public sealed class PlayerData : ScriptableObject
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