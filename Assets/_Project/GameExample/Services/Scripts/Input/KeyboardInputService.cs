using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class KeyboardInputService : IInputService,
        ITickable
    {
        public event Action<Vector2> OnMoveInput;
        public event Action<Vector2> OnShootInput;

        void ITickable.Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootInput?.Invoke(Vector2.up);
            }

            OnMoveInput?.Invoke(new Vector2(Input.GetAxis("Horizontal"), 0));
        }
    }
}