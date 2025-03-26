using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    [RequireComponent(typeof(EnemyMoveAgent))]
    public sealed class EnemyAttackAgent : GameMonoBehaviour,
        IGameFixedTickable
    {
        private GameObject target;

        private WeaponComponent weaponComponent;
        private EnemyMoveAgent enemyMoveAgent;

        private void Awake()
        {
            this.weaponComponent = this.gameObject.GetComponent<WeaponComponent>();
            this.enemyMoveAgent = this.gameObject.GetComponent<EnemyMoveAgent>();
        }

        public void SetTarget(GameObject target)
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
            Vector2 direction = ((Vector2) this.target.transform.position - startPosition).normalized;
            this.weaponComponent.Shoot(direction);
        }
    }
}