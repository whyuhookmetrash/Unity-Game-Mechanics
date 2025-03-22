using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletManager : MonoBehaviour
    {

        [SerializeField]
        private LevelBounds levelBounds;

        private readonly HashSet<Bullet> activeBullets = new();
        private readonly List<Bullet> cache = new();
        
        private void FixedUpdate()
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
        }

        public void RemoveActiveBullet(Bullet bullet)
        {
            this.activeBullets.Remove(bullet);
        }
        
    }
}