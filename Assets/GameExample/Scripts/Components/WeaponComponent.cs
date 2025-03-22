using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(TeamComponent))]
    public sealed class WeaponComponent : GameMonoBehaviour,
        IGameFixedTickable
    {
        [SerializeField] 
        private BulletFactory bulletFactory;

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

        private void Awake()
        {
            bulletFactory = FindAnyObjectByType<BulletFactory>();
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            this.rechargeCurrentTime -= Time.fixedDeltaTime;
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
                isPlayer = this.gameObject.GetComponent<TeamComponent>().IsPlayer,
                physicsLayer = (int)this.bulletConfig.physicsLayer,
                color = this.bulletConfig.color,
                damage = this.bulletConfig.damage,
                position = this.firePoint.position,
                velocity = this.bulletConfig.speed * shootDirection
            });
        }

    }
}