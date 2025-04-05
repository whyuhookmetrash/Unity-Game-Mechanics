using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour,
        IMonoHandler
    {
        public event Action<Bullet> OnBulletDestroy;

        [NonSerialized] public int damage;

        public Rigidbody2D RigidBody => rigidBody;
        [SerializeField] private Rigidbody2D rigidBody;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public TeamComponent TeamComponent => teamComponent;

        private MoveComponent moveComponent;
        private TeamComponent teamComponent;

        public void Construct(MoveComponent moveComponent, TeamComponent teamComponent)
        {
            this.moveComponent = moveComponent;
            this.teamComponent = teamComponent;
        }

        public void Init(Args args)
        {
            this.gameObject.layer = (int)args.bulletData.physicsLayer;
            this.transform.position = args.at.position;
            this.spriteRenderer.color = args.bulletData.color;
            this.damage = args.bulletData.damage;
            this.teamComponent.Init(args.combatTeam);
            this.moveComponent.Init(args.bulletData.speed, args.moveDirection);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision is IDamageTaker damageTaker)
            {
                BulletUtils.DealDamage(this, damageTaker);
            }
            this.DestroySelf();
        }

        private void OnEnable() => this.moveComponent.OnEnable(); 

        private void OnDisable() => this.moveComponent.OnDisable();

        private void OnDestroy() => this.moveComponent.OnDestroy();

        public void DestroySelf() => this.OnBulletDestroy?.Invoke(this);

        public sealed class Args
        {
            public BulletData bulletData;
            public Transform at;
            public Vector2 moveDirection;
            public Team combatTeam;
        }
    }
}