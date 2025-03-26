using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletFactory : GameMonoBehaviour
    {
        [SerializeField]
        private BulletPool bulletPool;

        public event Action<Bullet> OnBulletCreate;

        public Bullet CreateBulletByArgs(Args args)
        {
            Bullet bullet = bulletPool.GetBullet();

            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);

            this.OnBulletCreate?.Invoke(bullet);

            return bullet;
        }

        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}