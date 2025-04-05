using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerFactory :
        IFactory<Player.Args, Player>
    {
        private readonly IPrefabFactory prefabFactory;
        private readonly GamePrefabsService gamePrefabsService;
        private readonly WorldTransformProvider worldTransformProvider;
        private readonly IFactory<Rigidbody2D, MoveComponent> moveComponentFactory;
        private readonly IFactory<TeamComponent> teamComponentFactory;
        private readonly IFactory<HealthComponent> healthComponentFactory;
        private readonly IFactory<TeamComponent, WeaponComponent> weaponComponentFactory;

        public PlayerFactory(
            IPrefabFactory prefabFactory,
            GamePrefabsService gamePrefabsService,
            WorldTransformProvider worldTransformProvider,
            IFactory<TeamComponent> teamComponentFactory,
            IFactory<HealthComponent> healthComponentFactory,
            IFactory<TeamComponent, WeaponComponent> weaponComponentFactory,
            IFactory<Rigidbody2D, MoveComponent> moveComponentFactory
            )
        {
            this.prefabFactory = prefabFactory;
            this.gamePrefabsService = gamePrefabsService;
            this.worldTransformProvider = worldTransformProvider;
            this.moveComponentFactory = moveComponentFactory;
            this.teamComponentFactory = teamComponentFactory;
            this.healthComponentFactory = healthComponentFactory;
            this.weaponComponentFactory = weaponComponentFactory;
        }

        public Player Create(Player.Args args)
        {
            Player player = this.prefabFactory.CreatePrefab<Player>(this.gamePrefabsService.PlayerPrefab, this.worldTransformProvider.enemyPoolTransform);
            MoveComponent moveComponent = this.moveComponentFactory.Create(player.RigidBody);
            HealthComponent healthComponent = this.healthComponentFactory.Create();
            TeamComponent teamComponent = this.teamComponentFactory.Create();
            WeaponComponent weaponComponent = this.weaponComponentFactory.Create(teamComponent);
            player.Construct(moveComponent, healthComponent, teamComponent, weaponComponent);
            player.Init(args);
            return player;
        }
    }
}