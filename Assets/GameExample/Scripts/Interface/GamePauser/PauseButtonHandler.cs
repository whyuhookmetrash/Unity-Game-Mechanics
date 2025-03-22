using System;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PauseButtonHandler : GameMonoBehaviour,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField]
        private PauseButtonView pauseButtonView;

        public event Action OnButtonClick;
        public void OnClick()
        {
            OnButtonClick?.Invoke();
            if (GameCycle.Instance.MainState == GameState.PLAY)
            {
                GameCycle.Instance.PauseGame();
            }
            else if (GameCycle.Instance.MainState == GameState.PAUSE)
            {
                GameCycle.Instance.ResumeGame();
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