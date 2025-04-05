using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class RealTimeRegistration
    {
        private readonly List<IRealTimeFactory> realTimeFactories = new();
        private readonly GameCycle gameCycle;
        private readonly TickableManager tickableManager;
        private readonly GameTickableManager gameTickableManager;

        public RealTimeRegistration(
            [InjectLocal(Optional = true)] List<IRealTimeFactory> realTimeFactories,
            GameCycle gameCycle,
            [InjectLocal] TickableManager tickableManager,
            [InjectLocal] GameTickableManager gameTickableManager
            )
        {
            this.realTimeFactories = realTimeFactories;
            this.gameCycle = gameCycle;
            this.tickableManager = tickableManager;
            this.gameTickableManager = gameTickableManager;
        }
        /*
        Пока не придумал других решений, чтобы не добавлять лишний [Inject] помимо конструктора,
        т.к. регистрация фабрик должна выполняться как минимум до всех Unity Start() / Zenject IInizializable()
        */
        [Inject]
        private void RegisterFactories()
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
            if (value is IGameListener gameListener)
            {
                this.gameCycle.AddListener(gameListener);
            }
            if (value is ITickable tickable)
            {
                this.tickableManager.Add(tickable);
            }
            if (value is IFixedTickable fixedTickable)
            {
                this.tickableManager.AddFixed(fixedTickable);
            }
            if (value is ILateTickable lateTickable)
            {
                this.tickableManager.AddLate(lateTickable);
            }
            if (value is IGameTickable gameTickable)
            {
                this.gameTickableManager.Add(gameTickable);
            }
            if (value is IGameFixedTickable gameFixedTickable)
            {
                this.gameTickableManager.AddFixed(gameFixedTickable);
            }
            if (value is IGameLateTickable gameLateTickable)
            {
                this.gameTickableManager.AddLate(gameLateTickable);
            }
        }

        private void UnRegisterInterfaces(IRealTimeRegistered value)
        {
            if (value is IGameListener gameListener)
            {
                this.gameCycle.RemoveListener(gameListener);
            }
            if (value is ITickable tickable)
            {
                this.tickableManager.Remove(tickable);
            }
            if (value is IFixedTickable fixedTickable)
            {
                this.tickableManager.RemoveFixed(fixedTickable);
            }
            if (value is ILateTickable lateTickable)
            {
                this.tickableManager.RemoveLate(lateTickable);
            }
            if (value is IGameTickable gameTickable)
            {
                this.gameTickableManager.Remove(gameTickable);
            }
            if (value is IGameFixedTickable gameFixedTickable)
            {
                this.gameTickableManager.RemoveFixed(gameFixedTickable);
            }
            if (value is IGameLateTickable gameLateTickable)
            {
                this.gameTickableManager.RemoveLate(gameLateTickable);
            }
        }
    }
}