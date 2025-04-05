using System.Collections.Generic;
using System;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgentFactory :
        IFactory<MoveComponent, EnemyMoveAgent>,
        IRealTimeFactory
    {
        public event Action<IRealTimeRegistered> OnValueCreate;

        private readonly IInstantiator _instantiator;

        public EnemyMoveAgentFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public EnemyMoveAgent Create(MoveComponent moveComponent)
        {
            EnemyMoveAgent instance = _instantiator.Instantiate<EnemyMoveAgent>(new List<object>() { moveComponent });
            this.OnValueCreate?.Invoke(instance);
            return instance;
        }
    }
}