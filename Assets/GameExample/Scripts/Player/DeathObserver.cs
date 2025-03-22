using UnityEngine;

namespace ShootEmUp.Player
{
    public sealed class DeathObserver : MonoBehaviour
    {
        [SerializeField]
        private HitPointsComponent hitPointsComponent;

        [SerializeField]
        private GameManager gameManager;


        private void OnEnable()
        {
            this.hitPointsComponent.OnHpEmpty += this.OnPlayerDeath;
        }

        private void OnDisable()
        {
            this.hitPointsComponent.OnHpEmpty -= this.OnPlayerDeath;
        }

        private void OnPlayerDeath(GameObject _) => this.gameManager.FinishGame();


    }
}