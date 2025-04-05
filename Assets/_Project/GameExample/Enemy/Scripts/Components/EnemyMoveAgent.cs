using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : RealTimeComponent,
        IGameFixedTickable
    {
        private const float DISTANCE_TO_REACH = 0.25f;

        public bool IsReached => this.isReached;
        private bool isReached;
        private Vector2 destination;

        private readonly MoveComponent moveComponent;

        public EnemyMoveAgent(MoveComponent moveComponent)
        {
            this.moveComponent = moveComponent;
        }

        public void SetDestination(Vector2 destination)
        {
            this.destination = destination;
            this.isReached = false;
            SetDirection((this.destination - (Vector2)this.moveComponent.RigidBody.transform.position).normalized);
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
            
            float distance = (this.destination - (Vector2) this.moveComponent.RigidBody.transform.position).magnitude;
            if (distance <= DISTANCE_TO_REACH)
            {
                this.isReached = true;
                SetDirection(Vector2.zero);
                return;
            }
        }
    }
}