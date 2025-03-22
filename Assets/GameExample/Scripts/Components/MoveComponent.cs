using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : GameMonoBehaviour,
        IGameFixedTickable
    {
 
        [SerializeField]
        private float speed = 5.0f;

        private new Rigidbody2D rigidbody2D;

        private Vector2 direction = Vector2.zero;

        private void Awake()
        {
            this.rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.direction = direction;
        }
        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            Vector2 nextPosition = this.rigidbody2D.position + direction * this.speed * Time.fixedDeltaTime;
            this.rigidbody2D.MovePosition(nextPosition);
        }

    }
}