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
            this.Container
                .Bind<LevelBounds>()
                .AsSingle()
                .WithArguments(levelBoundsConfig);

            this.Container
                .BindInterfacesTo<LevelBackground>()
                .AsSingle()
                .WithArguments(backgroundConfig);
        }

        private void BindEnemySystem()
        {
            this.Container
                .Bind<EnemyPositions>()
                .AsSingle()
                .WithArguments(enemyPositionsConfig);

            this.Container
                .BindInterfacesAndSelfTo<EnemyPool>()
                .AsSingle()
                .WithArguments(enemyPoolConfig);

            this.Container
               .Bind<EnemyFactory>()
               .AsSingle();

            this.Container
                .BindInterfacesTo<EnemySpawner>()
                .AsSingle()
                .WithArguments(enemySpawnerConfig);
        }

        private void BindBulletSystem()
        {
            this.Container
                .BindInterfacesAndSelfTo<BulletPool>()
                .AsSingle()
                .WithArguments(bulletPoolConfig);

            this.Container
                .Bind<BulletFactory>()
                .AsSingle();

            this.Container
                .BindInterfacesTo<BulletManager>()
                .AsSingle();
        }

        private void BindUserInterface()
        {
            this.Container
                .Bind<StartButtonHandler>()
                .FromComponentInHierarchy()
                .AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<GameStarterView>()
                .FromComponentInHierarchy()
                .AsSingle();

            this.Container
                .BindInterfacesTo<GameStarterModel>()
                .AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<PauseButtonView>()
                .FromComponentInHierarchy()
                .AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<PauseButtonHandler>()
                .FromComponentInHierarchy()
                .AsSingle();

            this.Container
                .BindInterfacesTo<PauseButtonController>()
                .AsSingle();
        }
    }
}