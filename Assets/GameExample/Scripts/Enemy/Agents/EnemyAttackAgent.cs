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

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {

            if (!this.gameObject.GetComponent<EnemyMoveAgent>().IsReached)
            {
                return;
            }

            if (!this.gameObject.GetComponent<WeaponComponent>().canShoot)
            {
                return;
            }

            this.Shoot();
        }

        private void Shoot()
        {
            WeaponComponent weaponComponent = this.gameObject.GetComponent<WeaponComponent>();
            Vector2 startPosition = weaponComponent.firePosition;
            Vector2 direction = ((Vector2) this.target.transform.position - startPosition).normalized;
            weaponComponent.Shoot(direction);
        }
    }
}