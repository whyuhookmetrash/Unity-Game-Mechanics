using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyConstructor : MonoBehaviour
    {
        private EnemyMoveAgent enemyMoveAgent;
        private EnemyAttackAgent enemyAttackAgent;

        [Inject]
        public void Construct(
            EnemyMoveAgent enemyMoveAgent,
            EnemyAttackAgent enemyAttackAgent)
        {
            this.enemyAttackAgent = enemyAttackAgent;
            this.enemyMoveAgent = enemyMoveAgent;
        }

        public void SetArgs(Transform destination, GameObject shootTarget)
        {
            this.enemyAttackAgent.SetTarget(shootTarget);
            this.enemyMoveAgent.SetDestination(destination.position);
        }
    }
}