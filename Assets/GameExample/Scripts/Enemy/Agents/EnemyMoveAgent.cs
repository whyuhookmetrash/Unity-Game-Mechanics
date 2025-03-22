using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(MoveComponent))]
    public sealed class EnemyMoveAgent : GameMonoBehaviour,
        IGameFixedTickable
    {
        public bool IsReached
        {
            get { return this.isReached; }
        }

        private bool isReached;

        private Vector2 destination;

        public void SetDestination(Vector2 destination)
        {
            this.destination = destination;
            this.isReached = false;
            SetDirection((this.destination - (Vector2)this.gameObject.transform.position).normalized);
        }

        private void SetDirection(Vector2 direction)
        {
            this.gameObject.GetComponent<MoveComponent>().ChangeDirection(direction);
        }
        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            if (this.isReached)
            {
                return;
            }
            
            float distance = (this.destination - (Vector2) this.transform.position).magnitude;
            if (distance <= 0.25f)
            {
                this.isReached = true;
                SetDirection(Vector2.zero);
                return;
            }
        }

    }
}