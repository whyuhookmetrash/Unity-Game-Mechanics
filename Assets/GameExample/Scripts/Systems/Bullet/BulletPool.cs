using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletPool :
        IInitializable
    {
        private const int INITIAL_COUNT = 50;

        private readonly DiContainer diContainer;
        private readonly Transform worldContainer;
        private readonly Transform poolContainer;
        private readonly GameObject bulletPrefab;

        private readonly Queue<Bullet> bulletPool = new();

        public BulletPool(DiContainer diContainer, BulletPoolConfig config)
        {
            this.diContainer = diContainer;
            this.worldContainer = config.worldContainer;
            this.poolContainer = config.poolContainer;
            this.bulletPrefab = config.bulletPrefab;
        }

        void IInitializable.Initialize()
        {
            for (var i = 0; i < INITIAL_COUNT; i++)
            {
                GameObject bulletObject = this.diContainer.InstantiatePrefab(this.bulletPrefab, this.poolContainer);
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                this.bulletPool.Enqueue(bullet);
            }
        }

        public Bullet GetBullet()
        {
            if (this.bulletPool.TryDequeue(out Bullet bullet))
            {
                bullet.transform.SetParent(this.worldContainer);
            }
            else
            {
                GameObject bulletObject = this.diContainer.InstantiatePrefab(this.bulletPrefab, this.worldContainer);
                bullet = bulletObject.GetComponent<Bullet>();
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

    [Serializable]
    public sealed class BulletPoolConfig
    {
        public Transform worldContainer;

        public Transform poolContainer;

        public GameObject bulletPrefab;
    }
}