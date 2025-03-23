using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : GameMonoBehaviour
    {
        public event Action<Bullet> OnBulletDestroy;

        [NonSerialized]
        public bool isPlayer;

        [NonSerialized]
        public int damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BulletUtils.DealDamage(this, collision.gameObject);
            this.Destroy();
        }
        /* QUESTION:
        Полагаю, в таком случае лучше сделать rigidBody2D.MovePosition и обработать все в IGameFixedTickable,
        как это сделано в MoveComponent, чем работать через linearVelocity и цеплять IGamePause, IGameResume, IGameFinish?
        или как тут будет сделать правильнее (см. RigidBody2DController.cs)
        */
        public void SetVelocity(Vector2 velocity)
        {
            this.rigidbody2D.linearVelocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void SetColor(Color color)
        {
            this.spriteRenderer.color = color;
        }

        public void Destroy()
        {
            this.OnBulletDestroy?.Invoke(this);
        }
    }
}