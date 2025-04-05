using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyPool :
        IInitializable
    {
        public event Action<Enemy> OnEnemySpawned;

        private const int INITIAL_COUNT = 10;

        private readonly Queue<Enemy> enemyPool = new();

        private readonly WorldTransformProvider worldTransformProvider;
        private readonly IFactory<Enemy.Args, Enemy> enemyFactory;

        public EnemyPool(
            WorldTransformProvider worldTransformProvider,
            IFactory<Enemy.Args, Enemy> enemyFactory
            )
        {
            this.worldTransformProvider = worldTransformProvider;
            this.enemyFactory = enemyFactory;
        }

        void IInitializable.Initialize()
        {
            for (var i = 0; i < INITIAL_COUNT; i++)
            {
                Enemy enemy = this.enemyFactory.Create(new Enemy.Args
                {
                    enemyData = new EnemyData
                    {
                        bulletData = new BulletData(),
                        combatTeam = Team.BLUE,
                        rechargeCooldown = 0f,
                        health = 3,
                        speed = 5f
                    },
                    at = worldTransformProvider.worldTransform,
                    destination = Vector2.zero,
                    shootTarget = worldTransformProvider.worldTransform
                });

                enemy.transform.SetParent(this.worldTransformProvider.enemyPoolTransform);
                this.enemyPool.Enqueue(enemy);
            }
        }

        public Enemy SpawnEnemyByArgs(Enemy.Args args)
        {
            if (this.enemyPool.TryDequeue(out Enemy enemy))
            {
                enemy.transform.SetParent(this.worldTransformProvider.enemyWorldTransform);
                enemy.Init(args);
            }
            else
            {
                enemy = this.enemyFactory.Create(args);
                enemy.transform.SetParent(this.worldTransformProvider.enemyWorldTransform);
            }

            enemy.OnEnemyDestroy += this.DespawnEnemy;

            this.OnEnemySpawned?.Invoke(enemy);
            return enemy;
        }

        private void DespawnEnemy(Enemy enemy)
        {
            enemy.OnEnemyDestroy -= this.DespawnEnemy;
            enemy.transform.SetParent(this.worldTransformProvider.enemyPoolTransform);
            this.enemyPool.Enqueue(enemy);
        }
    }
}