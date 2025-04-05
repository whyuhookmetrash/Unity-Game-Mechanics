using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class WeaponComponent : RealTimeComponent,
        IGameFixedTickable
    {
        private BulletData bulletData;
        private Transform firePoint;
        private float rechargeCountdown;

        public Vector2 firePosition => (Vector2)firePoint.transform.position;
        private float rechargeCurrentTime;
        public bool canShoot;

        private readonly BulletPool bulletPool;
        private readonly TeamComponent teamComponent;

        public WeaponComponent(BulletPool bulletPool, TeamComponent teamComponent)
        {
            this.bulletPool = bulletPool;
            this.teamComponent = teamComponent;
        }

        public void Init(BulletData bulletData, Transform firePoint, float rechargeCountdown)
        {
            this.bulletData = bulletData;
            this.firePoint = firePoint;
            this.rechargeCountdown = rechargeCountdown;
            this.rechargeCurrentTime = 0f;
        }

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

            this.bulletPool.SpawnBulletByArgs(new Bullet.Args
            {
                bulletData = this.bulletData,
                at = this.firePoint,
                moveDirection = shootDirection,
                combatTeam = this.teamComponent.CombatTeam
            });
        }
    }
}