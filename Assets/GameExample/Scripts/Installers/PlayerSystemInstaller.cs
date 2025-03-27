using ShootEmUp.Player;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "PlayerSystemInstaller",
        menuName = "Installers/New PlayerSystemInstaller")]
    public sealed class PlayerSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<DeathObserver>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<MoveController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<ShootController>()
                .AsSingle()
                .NonLazy();
        }
    }
}