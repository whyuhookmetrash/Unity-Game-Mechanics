using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletFactory :
        IFactory<Bullet.Args, Bullet>
    {
        private readonly IPrefabFactory prefabFactory;
        private readonly GamePrefabsService gamePrefabsService;
        private readonly WorldTransformProvider worldTransformProvider;
        private readonly IFactory<Rigidbody2D, MoveComponent> moveComponentFactory;
        private readonly IFactory<TeamComponent> teamComponentFactory;

        public BulletFactory(
            IPrefabFactory prefabFactory,
            GamePrefabsService gamePrefabsService,
            WorldTransformProvider worldTransformProvider,
            IFactory<Rigidbody2D, MoveComponent> moveComponentFactory,
            IFactory<TeamComponent> teamComponentFactory
            )
        {
            this.prefabFactory = prefabFactory;
            this.gamePrefabsService = gamePrefabsService;
            this.worldTransformProvider = worldTransformProvider;
            this.moveComponentFactory = moveComponentFactory;
            this.teamComponentFactory = teamComponentFactory;
        }

        public Bullet Create(Bullet.Args args)
        {
            Bullet bullet = this.prefabFactory.CreatePrefab<Bullet>(this.gamePrefabsService.BulletPrefab, this.worldTransformProvider.bulletPoolTransform);
            MoveComponent moveComponent = this.moveComponentFactory.Create(bullet.RigidBody);
            TeamComponent teamComponent = this.teamComponentFactory.Create();
            bullet.Construct(moveComponent, teamComponent);
            bullet.Init(args);
            return bullet;
        }
    }
}