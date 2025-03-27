
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemySpawner :
        IGameFixedTickable,
        IInitializable
    {
        private readonly EnemyPositions enemyPositions;
        private readonly EnemyFactory enemyFactory;

        private readonly GameObject shootTarget;
        private readonly float spawnTime;

        private float timeFromStart;
        private float nextSpawnTime;

        public EnemySpawner(EnemyPositions enemyPositions, EnemyFactory enemyFactory, EnemySpawnerConfig config)
        {
            this.enemyPositions = enemyPositions;
            this.enemyFactory = enemyFactory;
            this.shootTarget = config.shootTarget;
            this.spawnTime = config.spawnTime;
        }

        void IInitializable.Initialize()
        {
            timeFromStart = 0f;
            nextSpawnTime = spawnTime;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            timeFromStart += deltaTime;

            if(timeFromStart > nextSpawnTime)
            {
                var spawnCount = 1 + (int) ((timeFromStart - nextSpawnTime) / spawnTime);
                nextSpawnTime += spawnTime * spawnCount;

                for (int i = 0; i < spawnCount; i++)
                {
                    this.SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            var spawnPosition = this.enemyPositions.RandomSpawnPosition();
            var attackPosition = this.enemyPositions.RandomAttackPosition();
            GameObject enemy;
            if (this.enemyFactory.TryCreateEnemyByArgs(spawnPosition, attackPosition, this.shootTarget, out enemy))
            {
                //AddToActiveEnemies(enemy); //Я его убрал т.к. он пока нигде не используется
            }
        }
    }

    [Serializable]
    public sealed class EnemySpawnerConfig
    {
        public GameObject shootTarget;

        public float spawnTime;
    }
}