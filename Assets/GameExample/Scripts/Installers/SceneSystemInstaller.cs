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
            Container
                .BindInterfacesAndSelfTo<InputManager>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<Timer>()
                .AsTransient();
        }
    }
}