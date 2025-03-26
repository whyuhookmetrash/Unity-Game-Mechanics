using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : GameMonoBehaviour
    {
        private const int INITIAL_MAX_COUNT = 7;

        [SerializeField]
        private Transform worldContainer;

        [SerializeField]
        private Transform poolContainer;

        [SerializeField]
        private GameObject enemyPrefab;

        private readonly Queue<GameObject> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < INITIAL_MAX_COUNT; i++)
            {
                GameObject enemy = Instantiate(this.enemyPrefab, this.poolContainer);
                this.enemyPool.Enqueue(enemy);
            }
        }

        public GameObject GetEnemy()
        {
            if (!this.enemyPool.TryDequeue(out var enemy))
            {
                return null;
            }
            enemy.transform.SetParent(this.worldContainer);

            HitPointsComponent hitPointsComponent;
            if (enemy.TryGetComponent<HitPointsComponent>(out hitPointsComponent))
            {
                hitPointsComponent.OnHpEmpty += this.RemoveEnemy;
            }

            return enemy;
        }

        public void RemoveEnemy(GameObject enemy)
        {
            HitPointsComponent hitPointsComponent;
            if (enemy.TryGetComponent<HitPointsComponent>(out hitPointsComponent))
            {
                hitPointsComponent.OnHpEmpty -= this.RemoveEnemy;
            }

            enemy.transform.SetParent(this.poolContainer);
            this.enemyPool.Enqueue(enemy);
        }
    }
}