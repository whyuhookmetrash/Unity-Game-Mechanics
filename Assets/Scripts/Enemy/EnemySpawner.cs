using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyPositions enemyPositions;

        [SerializeField]
        private GameObject shootTarget;

        [SerializeField]
        private EnemyFactory enemyFactory;

        private readonly HashSet<GameObject> activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                this.SpawnEnemy();
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