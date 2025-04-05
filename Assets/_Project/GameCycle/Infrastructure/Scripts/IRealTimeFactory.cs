using System;

namespace ShootEmUp
{
    public interface IRealTimeFactory
    {
        public event Action<IRealTimeRegistered> OnValueCreate;
    }
}