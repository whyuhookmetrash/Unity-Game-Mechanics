using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletPool :
        IInitializable
    {
        public event Action<Bullet> OnBulletSpawned;

        private const int INITIAL_COUNT = 50;

        private readonly Queue<Bullet> bulletPool = new();

        private readonly WorldTransformProvider worldTransformProvider;
        private readonly IFactory<Bullet.Args, Bullet> bulletFactory;

        public BulletPool(
            WorldTransformProvider worldTransformProvider,
            IFactory<Bullet.Args, Bullet> bulletFactory
            )
        {
            this.worldTransformProvider = worldTransformProvider;
            this.bulletFactory = bulletFactory;
        }

        void IInitializable.Initialize()
        {
            for (var i = 0; i < INITIAL_COUNT; i++)
            {
                Bullet bullet = this.bulletFactory.Create(new Bullet.Args
                {
                    bulletData = new BulletData(),
                    at = worldTransformProvider.worldTransform,
                    combatTeam = Team.NONE,
                    moveDirection = Vector2.zero    
                });

                bullet.transform.SetParent(this.worldTransformProvider.bulletPoolTransform);
                this.bulletPool.Enqueue(bullet);
            }
        }

        public Bullet SpawnBulletByArgs(Bullet.Args args)
        {
            if (this.bulletPool.TryDequeue(out Bullet bullet))
            {
                bullet.transform.SetParent(this.worldTransformProvider.bulletWorldTransform);
                bullet.Init(args);
            }
            else
            {
                bullet = this.bulletFactory.Create(args);
                bullet.transform.SetParent(this.worldTransformProvider.bulletWorldTransform);
            }

            bullet.OnBulletDestroy += this.DespawnBullet;

            this.OnBulletSpawned?.Invoke(bullet);
            return bullet;
        }

        private void DespawnBullet(Bullet bullet)
        {
            bullet.OnBulletDestroy -= this.DespawnBullet;
            bullet.transform.SetParent(this.worldTransformProvider.bulletPoolTransform);
            this.bulletPool.Enqueue(bullet);
        }
    }
}