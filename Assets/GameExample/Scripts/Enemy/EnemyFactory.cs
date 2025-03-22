using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyFactory : GameMonoBehaviour
    {
        [SerializeField]
        private EnemyPool enemyPool;


        public GameObject CreateEnemyByArgs(Transform spawnPosition, Transform destination, GameObject shootTarget)
        {
            GameObject enemy = this.enemyPool.GetEnemy();
            if (enemy == null)
            {
                return null;
            }
            enemy.transform.position = spawnPosition.position;
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(destination.position);
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(shootTarget);
            /* 
             TODO: ������� enemy config � ����������� ��� ������ ���-�� ��,
             �.�. 8+ �� ����� ����� ������� �� ���� � 0 �� � ������� �� 1 �����
            */
            enemy.GetComponent<HitPointsComponent>().OnHpEmpty += this.OnEnemyDestroyed;
            return enemy;
        }

        private void OnEnemyDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= this.OnEnemyDestroyed;

            enemyPool.RemoveEnemy(enemy);
        }
    }
}