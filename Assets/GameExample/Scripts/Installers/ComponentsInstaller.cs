using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class ComponentsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            MoveComponent moveComponent;
            if (this.gameObject.TryGetComponent<MoveComponent>(out moveComponent))
            {
                this.Container
                    .BindInterfacesAndSelfTo<MoveComponent>()
                    .FromInstance(moveComponent)
                    .AsSingle();
            }

            TeamComponent teamComponent;
            if (this.gameObject.TryGetComponent<TeamComponent>(out teamComponent))
            {
                this.Container
                    .Bind<TeamComponent>()
                    .FromInstance(teamComponent)
                    .AsSingle();
            }

            WeaponComponent weaponComponent;
            if (this.gameObject.TryGetComponent<WeaponComponent>(out weaponComponent))
            {
                this.Container
                    .BindInterfacesAndSelfTo<WeaponComponent>()
                    .FromInstance(weaponComponent)
                    .AsSingle();
            }

            HitPointsComponent hitPointsComponent;
            if (this.gameObject.TryGetComponent<HitPointsComponent>(out hitPointsComponent))
            {
                this.Container
                    .Bind<HitPointsComponent>()
                    .FromInstance(hitPointsComponent)
                    .AsSingle();
            }
        }
    }
}