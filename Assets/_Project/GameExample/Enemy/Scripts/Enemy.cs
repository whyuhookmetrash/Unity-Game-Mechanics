using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class Enemy : MonoBehaviour,
        IMonoHandler,
        IDamageTaker
    {
        public event Action<Enemy> OnEnemyDestroy;

        TeamComponent IDamageTaker.TeamComponent => teamComponent;

        public Rigidbody2D RigidBody => rigidBody;
        public Transform FirePoint => firePoint;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Rigidbody2D rigidBody;

        private MoveComponent moveComponent;
        private HealthComponent healthComponent;
        private TeamComponent teamComponent;
        private WeaponComponent weaponComponent;
        private EnemyMoveAgent enemyMoveAgent;
        private EnemyAttackAgent enemyAttackAgent;

        public void Construct(
            MoveComponent moveComponent,
            HealthComponent healthComponent,
            TeamComponent teamComponent,
            WeaponComponent weaponComponent,
            EnemyMoveAgent enemyMoveAgent,
            EnemyAttackAgent enemyAttackAgent
            )
        {
            this.moveComponent = moveComponent;
            this.healthComponent = healthComponent;
            this.teamComponent = teamComponent;
            this.weaponComponent = weaponComponent;
            this.enemyMoveAgent = enemyMoveAgent;
            this.enemyAttackAgent = enemyAttackAgent;
        }

        public void Init(Args args)
        {
            this.transform.position = args.at.position;
            this.moveComponent.Init(args.enemyData.speed, Vector2.zero);
            this.healthComponent.Init(args.enemyData.health);
            this.teamComponent.Init(args.enemyData.combatTeam);
            this.healthComponent.OnHpEmpty += this.DestroySelf;
            this.weaponComponent.Init(args.enemyData.bulletData, this.firePoint, args.enemyData.rechargeCooldown);
            this.enemyMoveAgent.SetDestination(args.destination);
            this.enemyAttackAgent.SetTarget(args.shootTarget);
        }

        private void OnEnable()
        {
            this.moveComponent.OnEnable();
            this.weaponComponent.OnEnable();
            this.enemyMoveAgent.OnEnable();
            this.enemyAttackAgent.OnEnable();
        }

        private void OnDisable()
        {
            this.moveComponent.OnDisable();
            this.weaponComponent.OnDisable();
            this.enemyMoveAgent.OnDisable();
            this.enemyAttackAgent.OnDisable();
        }

        private void OnDestroy()
        {
            this.healthComponent.OnHpEmpty -= this.DestroySelf;
            this.moveComponent.OnDestroy();
            this.weaponComponent.OnDestroy();
            this.enemyMoveAgent.OnDestroy();
            this.enemyAttackAgent.OnDestroy();
        }

        private void DestroySelf() => this.OnEnemyDestroy?.Invoke(this);

        void IDamageTaker.TakeDamage(int damage)
        {
            this.healthComponent.TakeDamage(damage);
        }

        public sealed class Args
        {
            public EnemyData enemyData;
            public Transform at;
            public Vector2 destination;
            public Transform shootTarget;
        }
    }
}