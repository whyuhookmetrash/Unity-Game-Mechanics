using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : RealTimeComponent,
        IGameFixedTickable
    {
        private Transform target;

        private readonly WeaponComponent weaponComponent;
        private readonly EnemyMoveAgent enemyMoveAgent;

        public EnemyAttackAgent(WeaponComponent weaponComponent, EnemyMoveAgent enemyMoveAgent)
        {
            this.weaponComponent = weaponComponent;
            this.enemyMoveAgent = enemyMoveAgent;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            if (!enemyMoveAgent.IsReached)
            {
                return;
            }

            if (!weaponComponent.canShoot)
            {
                return;
            }

            this.Shoot();
        }

        private void Shoot()
        {
            Vector2 startPosition = this.weaponComponent.firePosition;
            Vector2 direction = ((Vector2) this.target.position - startPosition).normalized;
            this.weaponComponent.Shoot(direction);
        }
    }
}