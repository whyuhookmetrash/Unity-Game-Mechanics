using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletManager :
        IGameFixedTickable,
        IInitializable,
        IDisposable
    {
        private readonly BulletPool bulletPool;
        private readonly LevelBounds levelBounds;

        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();

        public BulletManager(BulletPool bulletPool, LevelBounds levelBounds)
        {
            this.bulletPool = bulletPool;
            this.levelBounds = levelBounds;
        }

        void IInitializable.Initialize()
        {
            this.bulletPool.OnBulletSpawned += this.AddActiveBullet;
        }

        void IDisposable.Dispose()
        {
            this.bulletPool.OnBulletSpawned -= this.AddActiveBullet;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            this.cache.Clear();
            this.cache.AddRange(this.activeBullets);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var bullet = this.cache[i];

                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    bullet.DestroySelf();
                }
            }
        }

        private void AddActiveBullet(Bullet bullet)
        {
            this.activeBullets.Add(bullet);
            bullet.OnBulletDestroy += this.RemoveActiveBullet;
        }

        private void RemoveActiveBullet(Bullet bullet)
        {
            this.activeBullets.Remove(bullet);
            bullet.OnBulletDestroy -= this.RemoveActiveBullet;
        }
    }
}