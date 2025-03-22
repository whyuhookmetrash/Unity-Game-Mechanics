using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class ShootController : GameMonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField]
        private WeaponComponent weaponComponent;

        [SerializeField]
        private InputManager inputManager;


        void IGameStartListener.OnGameStart()
        {
            this.inputManager.OnShootInput += this.weaponComponent.Shoot;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.inputManager.OnShootInput -= this.weaponComponent.Shoot;
        }

    }
}