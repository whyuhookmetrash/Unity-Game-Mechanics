using System;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class PauseButtonHandler : MonoBehaviour,
        IGamePauseListener,
        IGameResumeListener
    {
        private PauseButtonView pauseButtonView;
        private GameCycle gameCycle;

        [Inject]
        public void Construct(PauseButtonView pauseButtonView, GameCycle gameCycle)
        {
            this.pauseButtonView = pauseButtonView;
            this.gameCycle = gameCycle;
        }

        public event Action OnButtonClick;

        
        public void OnClick()
        {
            OnButtonClick?.Invoke();
            if (this.gameCycle.MainState == GameState.PLAY)
            {
                this.gameCycle.PauseGame();
            }
            else if (this.gameCycle.MainState == GameState.PAUSE)
            {
                this.gameCycle.ResumeGame();
            }
        }

        void IGamePauseListener.OnGamePause()
        {
            pauseButtonView.SetText("Resume");
        }

        void IGameResumeListener.OnGameResume()
        {
            pauseButtonView.SetText("Pause");
        }
    }
}