using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    [CreateAssetMenu(
        fileName = "SceneSystemInstaller",
        menuName = "Installers/New SceneSystemInstaller")]
    public sealed class GameSystemInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GamePrefabsService gamePrefabsService;
        [SerializeField] private EnemySpawner.Data enemySpawnerData;
        [SerializeField] private PlayerData playerSpawnData;
        [SerializeField] private BackgroundData backgroundData;

        public override void InstallBindings()
        {
            this.Container
                .Bind<GamePrefabsService>()
                .FromScriptableObject(this.gamePrefabsService)
                .AsSingle();

            this.Container
                .Bind<EnemySpawner.Data>()
                .FromScriptableObject(this.enemySpawnerData)
                .AsSingle();

            this.Container
                .Bind<PlayerData>()
                .FromScriptableObject(this.playerSpawnData)
                .AsSingle();

            this.Container
                .Bind<BackgroundData>()
                .FromScriptableObject(this.backgroundData)
                .AsSingle();
        }
    }
}