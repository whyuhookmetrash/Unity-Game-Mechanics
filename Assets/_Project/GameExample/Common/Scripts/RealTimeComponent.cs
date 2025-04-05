using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public abstract class RealTimeComponent : 
        IRealTimeRegistered
    {
        public event Action<IRealTimeRegistered> OnValueEnable;
        public event Action<IRealTimeRegistered> OnValueDisable;
        public event Action<IRealTimeRegistered> OnValueDestroy;

        public virtual void OnEnable() => this.OnValueEnable?.Invoke(this); 

        public virtual void OnDisable() => this.OnValueDisable?.Invoke(this);

        public virtual void OnDestroy() => this.OnValueDestroy?.Invoke(this);
    }
}