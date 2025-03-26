using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : GameMonoBehaviour
        ,IGameFixedTickable
    {
        [SerializeField]
        private float speed = 5.0f;

        private Rigidbody2D rigidBody;

        private Vector2 direction = Vector2.zero;

        private void Awake()
        {
            this.rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.direction = direction;
            //this.rigidBody.linearVelocity = direction * this.speed;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            Vector2 nextPosition = this.rigidBody.position + direction * (this.speed * Time.fixedDeltaTime);
            this.rigidBody.MovePosition(nextPosition);
        }
    }
}