
using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameStarterModel : GameMonoBehaviour
    {
        [SerializeField]
        private StartButtonHandler startButton;

        [SerializeField]
        private GameStarterView gameStarterView;

        private void Awake()
        {
            this.startButton.OnButtonClick += this.StartGame;
        }

        private void StartGame()
        {
            gameStarterView.Active(true);
            StartCoroutine(StartGameEnumerator());
        }

        private IEnumerator StartGameEnumerator()
        {
            gameStarterView.SetText("3");
            yield return new WaitForSeconds(1);
            gameStarterView.SetText("2");
            yield return new WaitForSeconds(1);
            gameStarterView.SetText("1");
            yield return new WaitForSeconds(1);
            GameCycle.Instance.StartGame();
            gameStarterView.Active(false);
        }

        private void OnDestroy()
        {
            this.startButton.OnButtonClick -= this.StartGame;
        }
    }
}