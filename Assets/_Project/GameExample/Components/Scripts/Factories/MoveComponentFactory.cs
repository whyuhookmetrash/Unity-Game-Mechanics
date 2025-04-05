using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class MoveComponentFactory :
        IFactory<Rigidbody2D, MoveComponent>,
        IRealTimeFactory
    {
        public event Action<IRealTimeRegistered> OnValueCreate;

        private readonly IInstantiator instantiator;

        public MoveComponentFactory(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public MoveComponent Create(Rigidbody2D rigidBody)
        {
            MoveComponent instance = this.instantiator.Instantiate<MoveComponent>(new List<object>() { rigidBody });
            this.OnValueCreate?.Invoke(instance);
            return instance;
        }
    }
}