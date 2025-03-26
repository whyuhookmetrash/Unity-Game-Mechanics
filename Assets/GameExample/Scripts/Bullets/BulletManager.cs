using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : GameMonoBehaviour,
        IGameFixedTickable
    {
        [SerializeField]
        private LevelBounds levelBounds;

        [SerializeField]
        private BulletFactory bulletFactory;

        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();

        private void Awake()
        {
            this.bulletFactory.OnBulletCreate += this.AddActiveBullet;
        }

        private void OnDestroy()
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