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
        private readonly LevelBounds levelBounds;
        private readonly BulletFactory bulletFactory;

        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();

        public BulletManager(LevelBounds levelBounds, BulletFactory bulletFactory)
        {
            this.levelBounds = levelBounds;
            this.bulletFactory = bulletFactory;
        }

        void IInitializable.Initialize()
        {
            this.bulletFactory.OnBulletCreate += this.AddActiveBullet;
        }

        void IDisposable.Dispose()
        {
            this.bulletFactory.OnBulletCreate -= this.AddActiveBullet;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            this.cache.Clear();
            this.cache.AddRange(this.activeBullets);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var bullet = this.cache[i];

                bullet.OnMove(deltaTime);

                if (!this.levelBounds.InBounds(bullet.transform.position))
                {
                    bullet.Destroy();
                }
            }
        }

        public void AddActiveBullet(Bullet bullet)
        {
            this.activeBullets.Add(bullet);
            bullet.OnBulletDestroy += this.RemoveActiveBullet;
        }

        public void RemoveActiveBullet(Bullet bullet)
        {
            this.activeBullets.Remove(bullet);
            bullet.OnBulletDestroy -= this.RemoveActiveBullet;
        }
    }
}