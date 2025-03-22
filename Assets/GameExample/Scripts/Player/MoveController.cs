using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class MoveController : GameMonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField]
        private MoveComponent moveComponent;

        [SerializeField]
        private InputManager inputManager;

        void IGameStartListener.OnGameStart()
        {
            this.inputManager.OnMoveInput += this.moveComponent.ChangeDirection;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.inputManager.OnMoveInput -= this.moveComponent.ChangeDirection;
        }

    }
}