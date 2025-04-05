using Zenject;

namespace ShootEmUp
{
    public sealed class PlayerSpawnFactory : 
        IFactory<Player>
    {
        private readonly IFactory<Player.Args, Player> playerFactory;
        private readonly PlayerData playerSpawnData;
        private readonly WorldTransformProvider worldTransformProvider;

        public PlayerSpawnFactory(
            IFactory<Player.Args, Player> playerFactory,
            PlayerData playerSpawnData,
            WorldTransformProvider worldTransformProvider
            )
        {
            this.playerFactory = playerFactory;
            this.playerSpawnData = playerSpawnData;
            this.worldTransformProvider = worldTransformProvider;
        }

        public Player Create()
        {
            Player player = playerFactory.Create(new Player.Args
            {
                playerData = this.playerSpawnData,
                at = this.worldTransformProvider.playerSpawnTransform
            });
            player.transform.SetParent(worldTransformProvider.worldTransform);
            return player;
        }
    }
}