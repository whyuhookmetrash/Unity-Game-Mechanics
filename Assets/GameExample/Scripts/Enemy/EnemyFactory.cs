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

            //TODO: ������� enemy config � ����������� ��� ������ ���-�� ��, �.�. 8+ �� ����� ����� ������� �� ���� � 0 �� � ������� �� 1 �����

            this.OnEnemyCreate?.Invoke(enemy);
            return true;
        }
    }
}