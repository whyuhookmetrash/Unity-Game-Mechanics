using System;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PauseButtonController :
        IGameStartListener,
        IGameFinishListener
    {
        private readonly PauseButtonHandler pauseButtonHandler;

        public PauseButtonController(PauseButtonHandler pauseButtonHandler)
        {
            this.pauseButtonHandler = pauseButtonHandler;
        }

        void IGameStartListener.OnGameStart()
        {
            pauseButtonHandler.gameObject.SetActive(true);
        }

        void IGameFinishListener.OnGameFinish()
        {
            pauseButtonHandler.gameObject.SetActive(false);
        }
    }
}