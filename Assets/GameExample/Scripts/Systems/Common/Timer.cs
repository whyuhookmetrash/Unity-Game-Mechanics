using System;

namespace ShootEmUp
{
    public sealed class Timer
    {
        public event Action<int> OnSecondPass;
        public event Action OnTimerPass;

        private bool isTimer = false;
        private int currentSecond = 0;
        private float currentTime = 0f;
        private float goalTime;

        public void StartTimer(float goalTime)
        {
            this.isTimer = true;
            this.currentTime = 0f;
            this.currentSecond = 0;
            this.goalTime = goalTime;
        }

        public void OnFixedTick(float deltaTime)
        {
            if (!this.isTimer)
            {
                return;
            }

            this.currentTime += deltaTime;

            int second = (int)this.currentTime;
            if (second > this.currentSecond)
            {
                this.currentSecond = second;
                this.OnSecondPass?.Invoke(this.currentSecond);
            }
            if (this.currentTime > this.goalTime)
            {
                this.isTimer = false;
                this.OnTimerPass?.Invoke();
            }
        }
    }
}