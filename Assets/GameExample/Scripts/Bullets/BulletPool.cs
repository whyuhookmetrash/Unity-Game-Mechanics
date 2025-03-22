using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletPool : MonoBehaviour
    {
        [SerializeField]
        private int initialCount = 50;

        [SerializeField] 
        private Transform worldContainer;

        [SerializeField]
        private Transform poolContainer;

        [SerializeField]
        private Bullet bulletPrefab;

        private readonly Queue<Bullet> bulletPool = new();

        private void Awake()
        {
            for (var i = 0; i < this.initialCount; i++)
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

            return bullet;
        }


        public void DestroyBullet(Bullet bullet)
        {
            bullet.transform.SetParent(this.poolContainer);
            this.bulletPool.Enqueue(bullet);
        }

    }

}