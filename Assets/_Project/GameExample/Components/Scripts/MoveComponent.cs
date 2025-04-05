using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class MoveComponent : RealTimeComponent,
        IGameFixedTickable
    {
        private float speed;
        private Vector2 direction;

        public Rigidbody2D RigidBody => rigidBody;
        private readonly Rigidbody2D rigidBody;

        public MoveComponent(Rigidbody2D rigidBody)
        {
            this.rigidBody = rigidBody;
        }

        public void Init(float speed, Vector2 direction)
        {
            this.speed = speed;
            this.direction = direction;
        }

        public void ChangeDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        void IGameFixedTickable.FixedTick(float deltaTime)
        {
            Vector2 nextPosition = this.rigidBody.position + direction * (this.speed * deltaTime);
            this.rigidBody.MovePosition(nextPosition);
        }
    }
}