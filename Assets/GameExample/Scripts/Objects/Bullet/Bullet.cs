using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour,
        IGameFixedTickable
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

        private Vector2 velocity;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BulletUtils.DealDamage(this, collision.gameObject);
            this.Destroy();
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            Vector2 nextPosition = this.rigidbody2D.position + this.velocity * Time.fixedDeltaTime;
            this.rigidbody2D.MovePosition(nextPosition);
        }

        public void SetVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
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