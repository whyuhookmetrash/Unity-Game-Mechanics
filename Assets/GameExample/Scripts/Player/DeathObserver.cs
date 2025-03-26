using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class DeathObserver : GameMonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField]
        private HitPointsComponent hitPointsComponent;

        void IGameStartListener.OnGameStart()
        {
            this.hitPointsComponent.OnHpEmpty += this.OnPlayerDeath;
        }

        void IGameFinishListener.OnGameFinish()
        {
            this.hitPointsComponent.OnHpEmpty -= this.OnPlayerDeath;
        }

        private void OnPlayerDeath(GameObject _) => GameCycle.Instance.FinishGame();
    }
}