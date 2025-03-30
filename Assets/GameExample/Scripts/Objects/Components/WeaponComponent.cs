using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class WeaponComponent : MonoBehaviour,
        IGameFixedTickable
    {
        private BulletFactory bulletFactory;
        private TeamComponent teamComponent;

        [Inject]
        public void Construct(BulletFactory bulletFactory, TeamComponent teamComponent)
        {
            this.bulletFactory = bulletFactory;
            this.teamComponent = teamComponent;
        }

        [SerializeField] 
        private BulletConfig bulletConfig;

        [SerializeField]
        private Transform firePoint;

        [SerializeField]
        private float rechargeCountdown;

        private float rechargeCurrentTime;

        [HideInInspector]
        public bool canShoot;

        [HideInInspector]
        public Vector2 firePosition { get { return (Vector2)firePoint.transform.position; } }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            this.rechargeCurrentTime -= deltaTime;
            if (this.rechargeCurrentTime <= 0)
            {
                this.canShoot = true;
            }
        }

        public void Shoot(Vector2 shootDirection)
        {
            if (!this.canShoot)
            {
                return;
            }
            this.canShoot = false;
            this.rechargeCurrentTime = this.rechargeCountdown;

            shootDirection = shootDirection.normalized;
            this.bulletFactory.CreateBulletByArgs(new BulletFactory.Args
            {
                isPlayer = this.teamComponent.IsPlayer,
                physicsLayer = (int)this.bulletConfig.physicsLayer,
                color = this.bulletConfig.color,
                damage = this.bulletConfig.damage,
                position = this.firePoint.position,
                velocity = this.bulletConfig.speed * shootDirection
            });
        }
    }
}