using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class DeathObserver :
        IGameStartListener,
        IGameFinishListener
    {
        private readonly HitPointsComponent hitPointsComponent;
        private readonly GameCycle gameCycle;

        public DeathObserver(HitPointsComponent hitPointsComponent, GameCycle gameCycle)
        {
            this.hitPointsComponent = hitPointsComponent;
            this.gameCycle = gameCycle;
        }

        void IGameStartListener.OnGameStart()
        {
            this.hitPointsComponent.OnHpEmpty += this.OnPlayerDeath;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.hitPointsComponent.OnHpEmpty -= this.OnPlayerDeath;
        }

        private void OnPlayerDeath(GameObject _) => gameCycle.FinishGame();
    }
}