using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class MoveController : MonoBehaviour
    {
        [SerializeField]
        private MoveComponent moveComponent;

        [SerializeField]
        private InputManager inputManager;

        private void OnEnable()
        {
            this.inputManager.OnMoveInput += this.moveComponent.ChangeDirection;
        }

        private void OnDisable()
        {
            this.inputManager.OnMoveInput -= this.moveComponent.ChangeDirection;
        }
    }
}