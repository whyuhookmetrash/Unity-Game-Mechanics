using System;
using System.Collections.Generic;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager :
        IInitializable,
        IDisposable
    {
        private readonly EnemyPool enemyPool;

        private readonly HashSet<Enemy> activeEnemies = new();

        public int EnemyCount => enemyCount;
        private int enemyCount;

        public EnemyManager(EnemyPool enemyPool)
        {
            this.enemyPool = enemyPool;
        }

        void IInitializable.Initialize()
        {
            this.enemyPool.OnEnemySpawned += this.AddActiveEnemy;
            this.enemyCount = 0;
        }

        void IDisposable.Dispose()
        {
            this.enemyPool.OnEnemySpawned -= this.AddActiveEnemy;
        }

        private void AddActiveEnemy(Enemy enemy)
        {
            this.activeEnemies.Add(enemy);
            this.enemyCount += 1;
            enemy.OnEnemyDestroy += this.RemoveActiveEnemy;
        }

        private void RemoveActiveEnemy(Enemy enemy)
        {
            this.activeEnemies.Remove(enemy);
            enemy.OnEnemyDestroy -= this.RemoveActiveEnemy;
            this.enemyCount -= 1;
        }
    }
}