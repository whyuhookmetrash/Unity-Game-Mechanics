using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveController :
        IGameStartListener,
        IGameFinishListener
    {
        private readonly Player player;
        private readonly IInputService inputService;

        public MoveController(Player player, IInputService inputService)
        {
            this.player = player;
            this.inputService = inputService;
        }

        void IGameStartListener.OnGameStart()
        {
            this.inputService.OnMoveInput += this.player.MoveComponent.ChangeDirection;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.inputService.OnMoveInput -= this.player.MoveComponent.ChangeDirection;
        }
    }
}