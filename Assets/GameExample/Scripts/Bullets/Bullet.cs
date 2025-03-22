using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDestroy;

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
            this.OnDestroy?.Invoke(this);
        }
    }
}