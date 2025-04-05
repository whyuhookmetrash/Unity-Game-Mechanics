using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemySpawner :
        IGameFixedTickable,
        IInitializable
    {
        private readonly EnemyPositionSystem enemyPositionSystem;
        private readonly EnemyPool enemyPool;
        private readonly EnemyManager enemyManager;
        private readonly LazyInject<Player> player;
        private readonly EnemySpawner.Data data;

        private float spawnTime;
        private int maxEnemyCount;
        private float timeFromStart;
        private float nextSpawnTime;

        public EnemySpawner(
            EnemyPositionSystem enemyPositionSystem,
            EnemyPool enemyPool,
            EnemyManager enemyManager,
            EnemySpawner.Data data,
            LazyInject<Player> player
            )
        {
            this.enemyPositionSystem = enemyPositionSystem;
            this.enemyPool = enemyPool;
            this.enemyManager = enemyManager;
            this.data = data;
            this.player = player;
        }

        void IInitializable.Initialize()
        {
            this.timeFromStart = 0f;
            this.nextSpawnTime = this.spawnTime;
            this.spawnTime = this.data.spawnTime;
            this.maxEnemyCount = this.data.maxEnemyCount;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            if (this.enemyManager.EnemyCount < this.maxEnemyCount) {
                timeFromStart += deltaTime;

                if(timeFromStart > nextSpawnTime)
                {
                    var spawnCount = 1 + (int) ((timeFromStart - nextSpawnTime) / spawnTime);
                    nextSpawnTime += spawnTime * spawnCount;

                    for (int i = 0; i < spawnCount; i++)
                    {
                        if (this.enemyManager.EnemyCount < this.maxEnemyCount)
                        {
                            this.SpawnEnemy();
                        }
                    }
                }
            }
        }

        private void SpawnEnemy()
        {
            var spawnPosition = this.enemyPositionSystem.RandomSpawnPosition();
            var attackPosition = this.enemyPositionSystem.RandomAttackPosition();
            this.enemyPool.SpawnEnemyByArgs(new Enemy.Args
            {
                enemyData = this.data.enemyData,
                at = spawnPosition,
                destination = attackPosition.position,
                shootTarget = this.player.Value.transform 
            });
        }

        [CreateAssetMenu(fileName = "EnemySpawnerData", menuName = "Enemy/EnemySpawnerData")]
        public sealed class Data : ScriptableObject
        {
            public EnemyData enemyData;
            public float spawnTime;
            public int maxEnemyCount;
        }
    }
}