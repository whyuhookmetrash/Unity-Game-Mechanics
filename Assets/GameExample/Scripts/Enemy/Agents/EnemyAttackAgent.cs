using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(WeaponComponent))]
    [RequireComponent(typeof(EnemyMoveAgent))]
    public sealed class EnemyAttackAgent : MonoBehaviour
    {

        private GameObject target;

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        private void FixedUpdate()
        {
            /* 
            Ќа сколько тактично вызывать GetComponent на каждом кадре игры?
            ≈сли не рассматривать DI, то не лучше ли было бы закешировать переменную enemyMoveAgent и weaponComponent?
            —делать private пол€ и опрокинуть их в Awake или сделать private и закидывать через serializefield(второе кажетс€ не удобным)?
             */
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