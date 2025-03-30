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

            //TODO: ������� enemy config � ����������� ��� ������ ���-�� ��, �.�. 8+ �� ����� ����� ������� �� ���� � 0 �� � ������� �� 1 �����

            this.OnEnemyCreate?.Invoke(enemy);
            return true;
        }
    }
}