using UnityEngine;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "BulletConfig",
        menuName = "Bullets/New BulletConfig"
    )]
    public sealed class BulletData : ScriptableObject
    {
        [Header("Common")]
        public PhysicsLayer physicsLayer;
        public Color color;

        [Header("MoveComponent")]
        public float speed;

        [Header("Bullet Settings")]
        public int damage;
    }
}