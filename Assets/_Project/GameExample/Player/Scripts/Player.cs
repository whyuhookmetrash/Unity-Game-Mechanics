using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Player : MonoBehaviour,
        IMonoHandler,
        IDamageTaker
    {
        public event Action<Player> OnPlayerDestroy;

        TeamComponent IDamageTaker.TeamComponent => teamComponent;
        public MoveComponent MoveComponent => moveComponent;
        public WeaponComponent WeaponComponent => weaponComponent;

        public Rigidbody2D RigidBody => rigidBody;
        public Transform FirePoint => firePoint;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Rigidbody2D rigidBody;

        private MoveComponent moveComponent;
        private HealthComponent healthComponent;
        private TeamComponent teamComponent;
        private WeaponComponent weaponComponent;

        public void Construct(
            MoveComponent moveComponent,
            HealthComponent healthComponent,
            TeamComponent teamComponent,
            WeaponComponent weaponComponent
            )
        {
            this.moveComponent = moveComponent;
            this.healthComponent = healthComponent;
            this.teamComponent = teamComponent;
            this.weaponComponent = weaponComponent;
        }

        public void Init(Args args)
        {
            this.transform.position = args.at.position;
            this.moveComponent.Init(args.playerData.speed, Vector2.zero);
            this.healthComponent.Init(args.playerData.health);
            this.teamComponent.Init(args.playerData.combatTeam);
            this.healthComponent.OnHpEmpty += this.DestroySelf;
            this.weaponComponent.Init(args.playerData.bulletData, this.firePoint, args.playerData.rechargeCooldown);
        }

        private void OnEnable()
        {
            this.moveComponent.OnEnable();
            this.weaponComponent.OnEnable();
        }

        private void OnDisable()
        {
            this.moveComponent.OnDisable();
            this.weaponComponent.OnDisable();
        }

        private void OnDestroy()
        {
            this.healthComponent.OnHpEmpty -= this.DestroySelf;
            this.moveComponent.OnDestroy();
            this.weaponComponent.OnDestroy();
        }

        private void DestroySelf() => this.OnPlayerDestroy?.Invoke(this);

        void IDamageTaker.TakeDamage(int damage)
        {
            this.healthComponent.TakeDamage(damage);
        }

        public sealed class Args
        {
            public PlayerData playerData;
            public Transform at;
        }

    }
}