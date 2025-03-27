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
                Container
                    .BindInterfacesAndSelfTo<MoveComponent>()
                    .FromInstance(moveComponent)
                    .AsSingle();
            }

            TeamComponent teamComponent;
            if (this.gameObject.TryGetComponent<TeamComponent>(out teamComponent))
            {
                Container
                    .Bind<TeamComponent>()
                    .FromInstance(teamComponent)
                    .AsSingle();
            }

            WeaponComponent weaponComponent;
            if (this.gameObject.TryGetComponent<WeaponComponent>(out weaponComponent))
            {
                Container
                    .BindInterfacesAndSelfTo<WeaponComponent>()
                    .FromInstance(weaponComponent)
                    .AsSingle();
            }

            HitPointsComponent hitPointsComponent;
            if (this.gameObject.TryGetComponent<HitPointsComponent>(out hitPointsComponent))
            {
                Container
                    .Bind<HitPointsComponent>()
                    .FromInstance(hitPointsComponent)
                    .AsSingle();
            }
        }
    }
}