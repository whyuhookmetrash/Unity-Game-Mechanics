using Zenject;
using UnityEngine;
using static ShootEmUp.LevelBackground;
using System;

namespace ShootEmUp
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private EnemyPositionProvider enemyPositionProvider;
        [SerializeField] private WorldTransformProvider worldTransformProvider;

        public override void InstallBindings()
        {
            BindInfrastructure();

            BindProviders();

            BindLevelSystem();

            BindCommonSystem();

            BindComponentSystem();

            BindEnemySystem();

            BindBulletSystem();

            BindPlayerSystem();

            BindUserInterface();
        }

        private void BindInfrastructure()
        {
            this.Container
                .Bind<GameCycle>()
                .AsSingle();

            this.Container
                .Bind<GameTickableManager>()
                .AsSingle();

            this.Container
                .Bind<RealTimeRegistration>()
                .AsSingle()
                .NonLazy();
        }

        private void BindProviders()
        {
            this.Container
                .Bind<WorldTransformProvider>()
                .FromInstance(worldTransformProvider)
                .AsSingle();

            this.Container
                .Bind<EnemyPositionProvider>()
                .FromInstance(enemyPositionProvider)
                .AsSingle();
        }

        private void BindLevelSystem()
        {
            this.Container
                .Bind<LevelBounds>()
                .AsSingle();

            this.Container
                .BindInterfacesTo<LevelBackground>()
                .AsSingle();
        }

        private void BindCommonSystem()
        {
            this.Container
                .Bind<IPrefabFactory>()
                .To<PrefabFactory>()
                .AsSingle();

            this.Container
                .Bind(typeof(IFactory<Timer>), typeof(IRealTimeFactory), typeof(TimerFactory))
                .To<TimerFactory>()
                .AsSingle();

            this.Container
                .Bind<Timer>()
                .FromFactory<TimerSpawnFactory>()
                .AsTransient();

            this.Container
                .BindInterfacesTo<KeyboardInputService>()
                .AsSingle();
        }

        private void BindComponentSystem()
        {
            this.Container
                .Bind(typeof(IFactory<Rigidbody2D, MoveComponent>), typeof(IRealTimeFactory))
                .To<MoveComponentFactory>()
                .AsSingle();

            this.Container
                .Bind(typeof(IFactory<TeamComponent, WeaponComponent>), typeof(IRealTimeFactory))
                .To<WeaponComponentFactory>()
                .AsSingle();

            this.Container
                .Bind<IFactory<TeamComponent>>()
                .To<TeamComponentFactory>()
                .AsSingle();

            this.Container
                .Bind<IFactory<HealthComponent>>()
                .To<HealthComponentFactory>()
                .AsSingle();
        }

        private void BindEnemySystem()
        {
            this.Container
                .Bind<IFactory<Enemy.Args, Enemy>>()
                .To<EnemyFactory>()
                .AsSingle();

            this.Container
                .Bind(typeof(IFactory<MoveComponent, EnemyMoveAgent>), typeof(IRealTimeFactory))
                .To<EnemyMoveAgentFactory>()
                .AsSingle();

            this.Container
                .Bind(typeof(IFactory<WeaponComponent, EnemyMoveAgent, EnemyAttackAgent>), typeof(IRealTimeFactory))
                .To<EnemyAttackAgentFactory>()
                .AsSingle();

            this.Container
                .Bind<EnemyPositionSystem>()
                .AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<EnemyManager>()
                .AsSingle();

            this.Container
                .BindInterfacesTo<EnemySpawner>()
                .AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<EnemyPool>()
                .AsSingle();
        }

        private void BindBulletSystem()
        {
            this.Container
                .Bind<IFactory<Bullet.Args, Bullet>>()
                .To<BulletFactory>()
                .AsSingle();

            this.Container
                .BindInterfacesAndSelfTo<BulletPool>()
                .AsSingle();

            this.Container
                .BindInterfacesTo<BulletManager>()
                .AsSingle();
        }

        private void BindPlayerSystem()
        {
            this.Container
                .Bind<IFactory<Player.Args, Player>>()
                .To<PlayerFactory>()
                .AsSingle();

            this.Container
                .Bind<Player>()
                .FromFactory<PlayerSpawnFactory>()
                .AsSingle();
                //.NonLazy();

            this.Container
                .BindInterfacesTo<DeathObserver>()
                .AsSingle();

            this.Container
                .BindInterfacesTo<MoveController>()
                .AsSingle();

            this.Container
                .BindInterfacesTo<ShootController>()
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