using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class ShootController : MonoBehaviour
    {
        [SerializeField]
        private WeaponComponent weaponComponent;

        [SerializeField]
        private InputManager inputManager;


        private void OnEnable()
        {
            this.inputManager.OnShootInput += this.weaponComponent.Shoot;
        }

        private void OnDisable()
        {
            this.inputManager.OnShootInput -= this.weaponComponent.Shoot;
        }

    }
}