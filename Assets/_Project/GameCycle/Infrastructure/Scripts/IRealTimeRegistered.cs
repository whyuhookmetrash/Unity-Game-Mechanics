using System;

namespace ShootEmUp
{
    public interface IRealTimeRegistered
    {
        public event Action<IRealTimeRegistered> OnValueEnable;
        public event Action<IRealTimeRegistered> OnValueDisable;
        public event Action<IRealTimeRegistered> OnValueDestroy;
    }
}