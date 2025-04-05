using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyFactory : 
        IFactory<Enemy.Args, Enemy>
    {
        private readonly IPrefabFactory prefabFactory;
        private readonly GamePrefabsService gamePrefabsService;
        private readonly WorldTransformProvider worldTransformProvider;
        private readonly IFactory<Rigidbody2D, MoveComponent> moveComponentFactory;
        private readonly IFactory<TeamComponent> teamComponentFactory;
        private readonly IFactory<HealthComponent> healthComponentFactory;
        private readonly IFactory<TeamComponent, WeaponComponent> weaponComponentFactory;
        private readonly IFactory<MoveComponent, EnemyMoveAgent> enemyMoveAgentFactory;
        private readonly IFactory<WeaponComponent, EnemyMoveAgent, EnemyAttackAgent> enemyAttackAgentFactory;

        public EnemyFactory(
            IPrefabFactory prefabFactory,
            GamePrefabsService gamePrefabsService,
            WorldTransformProvider worldTransformProvider,
            IFactory<TeamComponent> teamComponentFactory,
            IFactory<HealthComponent> healthComponentFactory,
            IFactory<TeamComponent, WeaponComponent> weaponComponentFactory,
            IFactory<Rigidbody2D, MoveComponent> moveComponentFactory,
            IFactory<MoveComponent, EnemyMoveAgent> enemyMoveAgentFactory,
            IFactory<WeaponComponent, EnemyMoveAgent, EnemyAttackAgent> enemyAttackAgentFactory
            )
        {
            this.prefabFactory = prefabFactory;
            this.gamePrefabsService = gamePrefabsService;
            this.worldTransformProvider = worldTransformProvider;
            this.moveComponentFactory = moveComponentFactory;
            this.teamComponentFactory = teamComponentFactory;
            this.healthComponentFactory = healthComponentFactory;
            this.weaponComponentFactory = weaponComponentFactory;
            this.enemyMoveAgentFactory = enemyMoveAgentFactory;
            this.enemyAttackAgentFactory = enemyAttackAgentFactory;
        }

        public Enemy Create(Enemy.Args args)
        {
            Enemy enemy = this.prefabFactory.CreatePrefab<Enemy>(this.gamePrefabsService.EnemyPrefab, this.worldTransformProvider.enemyPoolTransform);
            MoveComponent moveComponent = this.moveComponentFactory.Create(enemy.RigidBody);
            HealthComponent healthComponent = this.healthComponentFactory.Create();
            TeamComponent teamComponent = this.teamComponentFactory.Create();
            WeaponComponent weaponComponent = this.weaponComponentFactory.Create(teamComponent);
            EnemyMoveAgent enemyMoveAgent = this.enemyMoveAgentFactory.Create(moveComponent);
            EnemyAttackAgent enemyAttackAgent = this.enemyAttackAgentFactory.Create(weaponComponent, enemyMoveAgent);
            enemy.Construct(moveComponent, healthComponent, teamComponent, weaponComponent, enemyMoveAgent, enemyAttackAgent);
            enemy.Init(args);
            return enemy;
        }
    }
}