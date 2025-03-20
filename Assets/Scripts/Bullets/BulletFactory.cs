using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletFactory : MonoBehaviour
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

            bullet.OnDestroy += bulletPool.DestroyBullet;

            bulletManager.AddActiveBullet(bullet);
            bullet.OnDestroy += bulletManager.RemoveActiveBullet;

            bullet.OnDestroy += this.OnBulletDestroy;

            return bullet;
        }

        private void OnBulletDestroy(Bullet bullet)
        {
            bullet.OnDestroy -= bulletPool.DestroyBullet;
            bullet.OnDestroy -= bulletManager.RemoveActiveBullet;
            bullet.OnDestroy -= this.OnBulletDestroy;
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