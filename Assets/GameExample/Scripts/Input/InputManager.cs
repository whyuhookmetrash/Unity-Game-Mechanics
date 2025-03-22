using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InputManager : MonoBehaviour
    {
        public event Action<Vector2> OnMoveInput;
        public event Action<Vector2> OnShootInput;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShootInput?.Invoke(Vector2.up);
            }

            OnMoveInput?.Invoke(new Vector2(Input.GetAxis("Horizontal"), 0));
        }
       
    }
}