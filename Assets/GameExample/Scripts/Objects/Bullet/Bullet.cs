using System;
using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnBulletDestroy;

        [NonSerialized]
        public bool isPlayer;

        [NonSerialized]
        public int damage;

        private new Rigidbody2D rigidbody2D;
        private SpriteRenderer spriteRenderer;

        private Vector2 velocity;

        private void Awake()
        {
            this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
            this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BulletUtils.DealDamage(this, collision.gameObject);
            this.Destroy();
        }

        public void OnMove(float deltaTime)
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