using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class Timer : RealTimeComponent,
        IFixedTickable
    {
        public event Action<int> OnSecondPass;
        public event Action OnTimerPass;

        private int currentSecond = 0;
        private float currentTime = 0f;
        private float goalTime;

        public void SetTimer(float goalTime)
        {
            this.currentTime = 0f;
            this.currentSecond = 0;
            this.goalTime = goalTime;
        }

        void IFixedTickable.FixedTick()
        {
            this.currentTime += Time.fixedDeltaTime;

            int second = (int)this.currentTime;
            if (second > this.currentSecond)
            {
                this.currentSecond = second;
                this.OnSecondPass?.Invoke(this.currentSecond);
            }
            if (this.currentTime > this.goalTime)
            {
                this.OnTimerPass?.Invoke();
            }
        }
    }
}