using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IInputService
    {
        public event Action<Vector2> OnMoveInput;
        public event Action<Vector2> OnShootInput;
    }
}