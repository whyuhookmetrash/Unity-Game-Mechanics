using System.Collections.Generic;
using System;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgentFactory :
        IFactory<WeaponComponent, EnemyMoveAgent, EnemyAttackAgent>,
        IRealTimeFactory
    {
        public event Action<IRealTimeRegistered> OnValueCreate;

        private readonly IInstantiator _instantiator;

        public EnemyAttackAgentFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public EnemyAttackAgent Create(WeaponComponent weaponComponent, EnemyMoveAgent enemyMoveAgent)
        {
            EnemyAttackAgent instance = _instantiator.Instantiate<EnemyAttackAgent>(new List<object>() { weaponComponent, enemyMoveAgent });
            this.OnValueCreate?.Invoke(instance);
            return instance;
        }
    }
}