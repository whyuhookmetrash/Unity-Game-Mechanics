using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class WeaponComponentFactory :
        IFactory<TeamComponent, WeaponComponent>,
        IRealTimeFactory
    {
        public event Action<IRealTimeRegistered> OnValueCreate;

        private readonly IInstantiator instantiator;
        private readonly BulletPool bulletPool;

        public WeaponComponentFactory(IInstantiator instantiator, BulletPool bulletPool)
        {
            this.instantiator = instantiator;
            this.bulletPool = bulletPool;
        }

        public WeaponComponent Create(TeamComponent teamComponent)
        {
            WeaponComponent instance = this.instantiator.Instantiate<WeaponComponent>(new List<object>() { bulletPool, teamComponent });
            this.OnValueCreate?.Invoke(instance);
            return instance;
        }
    }
}