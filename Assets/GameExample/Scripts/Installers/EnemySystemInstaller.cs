using ShootEmUp.Player;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "EnemySystemInstaller",
        menuName = "Installers/New EnemySystemInstaller")]
    public sealed class EnemySystemInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<EnemyMoveAgent>()
                .AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<EnemyAttackAgent>()
                .AsSingle();
        }
    }
}