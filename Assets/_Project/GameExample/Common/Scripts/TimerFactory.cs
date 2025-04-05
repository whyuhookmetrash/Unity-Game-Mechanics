using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class TimerFactory : IFactory<Timer>,
        IRealTimeFactory
    {
        public event Action<IRealTimeRegistered> OnValueCreate;

        private readonly IInstantiator instantiator;

        public TimerFactory(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public Timer Create()
        {
            Timer instance = this.instantiator.Instantiate<Timer>();
            this.OnValueCreate?.Invoke(instance);
            return instance;
        }
    }
}