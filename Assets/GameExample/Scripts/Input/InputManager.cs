using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : GameMonoBehaviour,
        ITickable
    {
        public event Action<Vector2> OnMoveInput;
        public event Action<Vector2> OnShootInput;

        void ITickable.Tick(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootInput?.Invoke(Vector2.up);
            }

            OnMoveInput?.Invoke(new Vector2(Input.GetAxis("Horizontal"), 0));

            if (Input.GetKeyDown(KeyCode.P))
            {
                GameCycle.Instance.PauseGame();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                GameCycle.Instance.ResumeGame();
            }
        }
       
    }
}