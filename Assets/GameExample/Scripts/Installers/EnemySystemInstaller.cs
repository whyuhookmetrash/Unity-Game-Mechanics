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
            Container
                .BindInterfacesAndSelfTo<EnemyMoveAgent>()
                .AsSingle();

            Container
                .BindInterfacesTo<EnemyAttackAgent>()
                .AsSingle();
        }
    }
}