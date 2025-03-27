using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyPool :
        IInitializable
    {
        private const int INITIAL_MAX_COUNT = 7;

        private readonly DiContainer diContainer;

        private readonly Transform worldContainer;
        private readonly Transform poolContainer;
        private readonly GameObject enemyPrefab;

        private readonly Queue<GameObject> enemyPool = new();

        public EnemyPool(DiContainer diContainer, EnemyPoolConfig config)
        {
            this.diContainer = diContainer;
            this.worldContainer = config.worldContainer;
            this.poolContainer = config.poolContainer;
            this.enemyPrefab = config.enemyPrefab;
        }

        void IInitializable.Initialize()
        {
            for (var i = 0; i < INITIAL_MAX_COUNT; i++)
            {
                GameObject enemy = this.diContainer.InstantiatePrefab(this.enemyPrefab, this.poolContainer);
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

    [Serializable]
    public sealed class EnemyPoolConfig
    {
        public Transform worldContainer;

        public Transform poolContainer;

        public GameObject enemyPrefab;
    }
}