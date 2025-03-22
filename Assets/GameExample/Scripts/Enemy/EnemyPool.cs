using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [SerializeField]
        private Transform worldContainer;

        [SerializeField]
        private Transform poolContainer;

        [SerializeField]
        private GameObject enemyPrefab;

        private readonly Queue<GameObject> enemyPool = new();
        
        private void Awake()
        {
            for (var i = 0; i < 7; i++)
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
            return enemy;
        }

        public void RemoveEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(this.poolContainer);
            this.enemyPool.Enqueue(enemy);
        }
    }
}