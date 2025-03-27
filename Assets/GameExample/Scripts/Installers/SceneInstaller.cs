using Zenject;
using UnityEngine;
using static ShootEmUp.LevelBackground;
using System;

namespace ShootEmUp
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private LevelBoundsConfig levelBoundsConfig;

        [SerializeField]
        private BackgroundConfig backgroundConfig;

        [SerializeField]
        private EnemySpawnerConfig enemySpawnerConfig;

        [SerializeField]
        private EnemyPositionsConfig enemyPositionsConfig;

        [SerializeField]
        private EnemyPoolConfig enemyPoolConfig;

        [SerializeField]
        private BulletPoolConfig bulletPoolConfig;

        public override void InstallBindings()
        {
            BindLevelSystem();

            BindEnemySystem();

            BindBulletSystem();

            BindUserInterface();
        }

        private void BindLevelSystem()
        {
            Container
                .Bind<LevelBounds>()
                .AsSingle()
                .WithArguments(levelBoundsConfig);

            Container
                .BindInterfacesTo<LevelBackground>()
                .AsSingle()
                .WithArguments(backgroundConfig);
        }

        private void BindEnemySystem()
        {
            Container
                .Bind<EnemyPositions>()
                .AsSingle()
                .WithArguments(enemyPositionsConfig);

            Container
                .BindInterfacesAndSelfTo<EnemyPool>()
                .AsSingle()
                .WithArguments(enemyPoolConfig);

            Container
               .Bind<EnemyFactory>()
               .AsSingle();

            Container
                .BindInterfacesTo<EnemySpawner>()
                .AsSingle()
                .WithArguments(enemySpawnerConfig);
        }

        private void BindBulletSystem()
        {
            Container
                .BindInterfacesAndSelfTo<BulletPool>()
                .AsSingle()
                .WithArguments(bulletPoolConfig);

            Container
                .Bind<BulletFactory>()
                .AsSingle();

            Container
                .BindInterfacesTo<BulletManager>()
                .AsSingle();
        }

        private void BindUserInterface()
        {
            Container
                .Bind<StartButtonHandler>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<GameStarterView>()
                .AsSingle();

            Container
                .BindInterfacesTo<GameStarterModel>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PauseButtonView>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PauseButtonHandler>()
                .AsSingle();

            Container
                .BindInterfacesTo<PauseButtonController>()
                .AsSingle();
        }
    }
}