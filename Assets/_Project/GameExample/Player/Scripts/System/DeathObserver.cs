using UnityEngine;

namespace ShootEmUp
{
    public sealed class DeathObserver :
        IGameStartListener,
        IGameFinishListener
    {
        private readonly Player player;
        private readonly GameCycle gameCycle;

        public DeathObserver(Player player, GameCycle gameCycle)
        {
            this.player = player;
            this.gameCycle = gameCycle;
        }

        void IGameStartListener.OnGameStart()
        {
            this.player.OnPlayerDestroy += this.OnPlayerDeath;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.player.OnPlayerDestroy -= this.OnPlayerDeath;
        }

        private void OnPlayerDeath(Player _) => gameCycle.FinishGame();
    }
}