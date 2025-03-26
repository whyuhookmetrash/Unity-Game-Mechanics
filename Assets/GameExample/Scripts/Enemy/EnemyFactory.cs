using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyFactory : GameMonoBehaviour
    {
        [SerializeField]
        private EnemyPool enemyPool;

        public event Action<GameObject> OnEnemyCreate;

        public bool TryCreateEnemyByArgs(Transform spawnPosition, Transform destination, GameObject shootTarget, out GameObject enemy)
        {
            enemy = this.enemyPool.GetEnemy();
            if (enemy == null)
            {
                return false;
            }
            enemy.transform.position = spawnPosition.position;

            EnemyMoveAgent enemyMoveAgent;
            if (enemy.TryGetComponent<EnemyMoveAgent>(out enemyMoveAgent))
            {
                enemyMoveAgent.SetDestination(destination.position);
            }

            EnemyAttackAgent enemyAttackAgent;
            if (enemy.TryGetComponent<EnemyAttackAgent>(out enemyAttackAgent))
            {
                enemyAttackAgent.SetTarget(shootTarget);
            }

            //TODO: Сделать enemy config и присваивать тут заного кол-во хп, т.к. 8+ по счету враги берутся из пула с 0 хп и умирают от 1 удара

            this.OnEnemyCreate?.Invoke(enemy);
            return true;
        }
    }
}