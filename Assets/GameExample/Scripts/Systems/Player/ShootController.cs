using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class ShootController :
        IGameStartListener,
        IGameFinishListener,
        IGamePauseListener,
        IGameResumeListener
    {
        private readonly WeaponComponent weaponComponent;
        private readonly InputManager inputManager;

        public ShootController(WeaponComponent weaponComponent, InputManager inputManager)
        {
            this.weaponComponent = weaponComponent;
            this.inputManager = inputManager;
        }

        void IGameStartListener.OnGameStart()
        {
            this.inputManager.OnShootInput += this.weaponComponent.Shoot;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.inputManager.OnShootInput -= this.weaponComponent.Shoot;
        }

        void IGamePauseListener.OnGamePause()
        {
            this.inputManager.OnShootInput -= this.weaponComponent.Shoot;
        }

        void IGameResumeListener.OnGameResume()
        {
            this.inputManager.OnShootInput += this.weaponComponent.Shoot;
        }
    }
}