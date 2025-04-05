using Zenject;

namespace ShootEmUp
{
    public sealed class ShootController :
        IGameStartListener,
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener
    {
        private readonly LazyInject<Player> player;
        private readonly IInputService inputService;

        public ShootController(LazyInject<Player> player, IInputService inputService)
        {
            this.player = player;
            this.inputService = inputService;
        }

        void IGameStartListener.OnGameStart()
        {
            this.inputService.OnShootInput += this.player.Value.WeaponComponent.Shoot;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.inputService.OnShootInput -= this.player.Value.WeaponComponent.Shoot;
        }

        void IGamePauseListener.OnGamePause()
        {
            this.inputService.OnShootInput -= this.player.Value.WeaponComponent.Shoot;
        }

        void IGameResumeListener.OnGameResume()
        {
            this.inputService.OnShootInput += this.player.Value.WeaponComponent.Shoot;
        }
    }
}