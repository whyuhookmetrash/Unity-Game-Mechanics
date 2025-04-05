using Zenject;

namespace ShootEmUp
{
    public sealed class DeathObserver :
        IGameStartListener,
        IGameFinishListener
    {
        private readonly LazyInject<Player> player;
        private readonly GameCycle gameCycle;

        public DeathObserver(LazyInject<Player> player, GameCycle gameCycle)
        {
            this.player = player;
            this.gameCycle = gameCycle;
        }

        void IGameStartListener.OnGameStart()
        {
            this.player.Value.OnPlayerDestroy += this.OnPlayerDeath;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.player.Value.OnPlayerDestroy -= this.OnPlayerDeath;
        }

        private void OnPlayerDeath(Player _) => gameCycle.FinishGame();
    }
}