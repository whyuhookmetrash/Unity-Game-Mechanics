using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletFactory : GameMonoBehaviour
    {
        [SerializeField]
        private BulletPool bulletPool;

        [SerializeField]
        private BulletManager bulletManager;


        public Bullet CreateBulletByArgs(Args args)
        {
            Bullet bullet = bulletPool.GetBullet();

            bullet.SetPosition(args.position);
            bullet.SetColor(args.color);
            bullet.SetPhysicsLayer(args.physicsLayer);
            bullet.damage = args.damage;
            bullet.isPlayer = args.isPlayer;
            bullet.SetVelocity(args.velocity);

            bullet.OnBulletDestroy += bulletPool.DestroyBullet;

            bulletManager.AddActiveBullet(bullet);
            bullet.OnBulletDestroy += bulletManager.RemoveActiveBullet;

            bullet.OnBulletDestroy += this.OnBulletDestroy;

            return bullet;
        }

        private void OnBulletDestroy(Bullet bullet)
        {
            bullet.OnBulletDestroy -= bulletPool.DestroyBullet;
            bullet.OnBulletDestroy -= bulletManager.RemoveActiveBullet;
            bullet.OnBulletDestroy -= this.OnBulletDestroy;
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