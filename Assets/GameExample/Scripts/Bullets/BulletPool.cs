using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : GameMonoBehaviour
    {
        private const int INITIAL_COUNT = 50;

        [SerializeField] 
        private Transform worldContainer;

        [SerializeField]
        private Transform poolContainer;

        [SerializeField]
        private Bullet bulletPrefab;

        private readonly Queue<Bullet> bulletPool = new();

        private void Awake()
        {
            for (var i = 0; i < INITIAL_COUNT; i++)
            {
                var bullet = Instantiate(this.bulletPrefab, this.poolContainer);
                this.bulletPool.Enqueue(bullet);
            }
        }

        public Bullet GetBullet()
        {
            if (this.bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this.worldContainer);
            }
            else
            {
                bullet = Instantiate(this.bulletPrefab, this.worldContainer);
            }

            bullet.OnBulletDestroy += this.DestroyBullet;

            return bullet;
        }

        public void DestroyBullet(Bullet bullet)
        {
            bullet.OnBulletDestroy -= this.DestroyBullet;
            bullet.transform.SetParent(this.poolContainer);
            this.bulletPool.Enqueue(bullet);
        }
    }
}