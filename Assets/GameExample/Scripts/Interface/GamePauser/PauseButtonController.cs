using System;
using TMPro;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PauseButtonController : GameMonoBehaviour,
        IGameStartListener,
        IGameFinishListener
    {
        [SerializeField]
        private PauseButtonView pauseButtonHandler;

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