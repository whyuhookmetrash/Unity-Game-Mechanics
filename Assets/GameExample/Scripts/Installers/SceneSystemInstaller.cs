using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "SceneSystemInstaller",
        menuName = "Installers/New SceneSystemInstaller")]
    public sealed class SceneSystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<InputManager>()
                .AsSingle();

            this.Container
                .Bind<Timer>()
                .AsTransient();

            this.Container
                .Bind<GameCycle>()
                .AsSingle();
        }
    }
}