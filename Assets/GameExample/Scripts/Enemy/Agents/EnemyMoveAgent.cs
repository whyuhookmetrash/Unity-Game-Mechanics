using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : GameMonoBehaviour,
        IGameFixedTickable
    {
        private const float DISTANCE_TO_REACH = 0.25f;

        public bool IsReached
        {
            get { return this.isReached; }
        }

        private bool isReached;

        private Vector2 destination;

        private MoveComponent moveComponent;

        private void Awake()
        {
            this.moveComponent = this.gameObject.GetComponent<MoveComponent>();    
        }

        public void SetDestination(Vector2 destination)
        {
            this.destination = destination;
            this.isReached = false;
            SetDirection((this.destination - (Vector2)this.gameObject.transform.position).normalized);
        }

        private void SetDirection(Vector2 direction)
        {
            this.moveComponent.ChangeDirection(direction);
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            if (this.isReached)
            {
                return;
            }
            
            float distance = (this.destination - (Vector2) this.transform.position).magnitude;
            if (distance <= DISTANCE_TO_REACH)
            {
                this.isReached = true;
                SetDirection(Vector2.zero);
                return;
            }
        }
    }
}