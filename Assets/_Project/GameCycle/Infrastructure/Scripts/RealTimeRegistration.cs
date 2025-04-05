using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class RealTimeRegistration :
        IInitializable
    {
        [Inject]
        private List<IRealTimeFactory> realTimeFactories;

        void IInitializable.Initialize()
        {
            foreach (var factory in this.realTimeFactories)
            {
                factory.OnValueCreate += this.OnValueCreate;
            }    
        }

        private void OnValueCreate(IRealTimeRegistered value)
        {
            value.OnValueEnable += this.OnValueEnable;
            value.OnValueDisable += this.OnValueDisable;
            value.OnValueDestroy += this.OnValueDestroy;
        }

        private void OnValueEnable(IRealTimeRegistered value)
        {
            RegisterInterfaces(value);
        }

        private void OnValueDisable(IRealTimeRegistered value)
        {
            UnRegisterInterfaces(value);
        }

        private void OnValueDestroy(IRealTimeRegistered value)
        {
            value.OnValueEnable -= this.OnValueEnable;
            value.OnValueDisable -= this.OnValueDisable;
            value.OnValueDestroy -= this.OnValueDestroy;
        }

        private void RegisterInterfaces(IRealTimeRegistered value)
        {
            Debug.Log("Registered Interfaces");
        }

        private void UnRegisterInterfaces(IRealTimeRegistered value)
        {
            Debug.Log("UnRegistered Interfaces");
        }
    }
}