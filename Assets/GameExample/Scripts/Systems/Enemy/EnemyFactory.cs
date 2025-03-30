using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyFactory
    {
        public event Action<GameObject> OnEnemyCreate;

        private readonly EnemyPool enemyPool;

        public EnemyFactory(EnemyPool enemyPool)
        {
            this.enemyPool = enemyPool;
        }

        public bool TryCreateEnemyByArgs(Transform spawnPosition, Transform destination, GameObject shootTarget, out GameObject enemy)
        {
            enemy = this.enemyPool.GetEnemy();
            if (enemy == null)
            {
                return false;
            }
            enemy.transform.position = spawnPosition.position;

            EnemyConstructor enemyConstructor;
            if (enemy.TryGetComponent<EnemyConstructor>(out enemyConstructor))
            {
                enemyConstructor.SetArgs(destination, shootTarget);
            }

            //TODO: Сделать enemy config и присваивать тут заного кол-во хп, т.к. 8+ по счету враги берутся из пула с 0 хп и умирают от 1 удара

            this.OnEnemyCreate?.Invoke(enemy);
            return true;
        }
    }
}