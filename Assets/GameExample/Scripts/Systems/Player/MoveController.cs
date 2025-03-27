using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class MoveController :
        IGameStartListener,
        IGameFinishListener
    {
        private readonly MoveComponent moveComponent;
        private readonly InputManager inputManager;

        public MoveController(MoveComponent moveComponent, InputManager inputManager)
        {
            this.moveComponent = moveComponent;
            this.inputManager = inputManager;
        }

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