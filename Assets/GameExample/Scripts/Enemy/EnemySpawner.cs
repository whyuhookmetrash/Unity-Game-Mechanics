
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : GameMonoBehaviour,
        IGameFixedTickable
    {
        [SerializeField]
        private EnemyPositions enemyPositions;

        [SerializeField]
        private GameObject shootTarget;

        [SerializeField]
        private EnemyFactory enemyFactory;

        [SerializeField]
        private float spawnTime;

        private float timeFromStart;
        private float nextSpawnTime;

        private readonly HashSet<GameObject> activeEnemies = new();

        private void Awake()
        {
            timeFromStart = 0f;
            nextSpawnTime = spawnTime;
        }

        /*
        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                this.SpawnEnemy();
            }
        }
        */

        // QUESTION: На сколько тактично заменить конструкцию IEnumerator Start на конструкцию такого вида?
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
            GameObject enemy = this.enemyFactory.CreateEnemyByArgs(spawnPosition, attackPosition, this.shootTarget);
            if (enemy != null)
            {
                if (this.activeEnemies.Add(enemy))
                {
                    enemy.GetComponent<HitPointsComponent>().OnHpEmpty += this.OnEnemyDestroyed;
                }
            }
        }

        private void OnEnemyDestroyed(GameObject enemy)
        {
            if (activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= this.OnEnemyDestroyed;
            }
        }

    }
}