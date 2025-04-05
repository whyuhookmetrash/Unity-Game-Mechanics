using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    internal sealed class GameCycleDebug : MonoBehaviour
    {
        [Inject]
        private GameCycle gameCycle;

        [Button("Start Game")]
        private void StartGame()
        {
            this.gameCycle.StartGame();
        }

        [Button("Pause Game")]
        private void PauseGame()
        {
            this.gameCycle.PauseGame();
        }

        [Button("Resume Game")]
        private void ResumeGame()
        {
            this.gameCycle.ResumeGame();
        }

        [Button("Finish Game")]
        private void FinishGame()
        {
            this.gameCycle.FinishGame();
        }
    }

}