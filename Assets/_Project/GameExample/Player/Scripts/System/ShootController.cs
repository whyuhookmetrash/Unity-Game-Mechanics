using UnityEngine;

namespace ShootEmUp
{
    public sealed class ShootController :
        IGameStartListener,
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener
    {
        private readonly Player player;
        private readonly IInputService inputService;

        public ShootController(Player player, IInputService inputService)
        {
            this.player = player;
            this.inputService = inputService;
        }

        void IGameStartListener.OnGameStart()
        {
            this.inputService.OnShootInput += this.player.WeaponComponent.Shoot;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.inputService.OnShootInput -= this.player.WeaponComponent.Shoot;
        }

        void IGamePauseListener.OnGamePause()
        {
            this.inputService.OnShootInput -= this.player.WeaponComponent.Shoot;
        }

        void IGameResumeListener.OnGameResume()
        {
            this.inputService.OnShootInput += this.player.WeaponComponent.Shoot;
        }
    }
}