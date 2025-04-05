
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameStarterModel :
        IInitializable,
        IDisposable
    {
        private const int START_TIMER = 3;

        private readonly StartButtonHandler startButton;
        private readonly GameStarterView gameStarterView;
        private readonly Timer timer;
        private readonly GameCycle gameCycle;

        public GameStarterModel(StartButtonHandler startButton, GameStarterView gameStarterView, Timer timer, GameCycle gameCycle)
        {
            this.startButton = startButton;
            this.gameStarterView = gameStarterView;
            this.timer = timer;
            this.gameCycle = gameCycle;
        }

        void IInitializable.Initialize()
        {
            this.startButton.OnButtonClick += this.StartGame;
        }

        private void StartGame()
        {
            gameStarterView.Active(true);
            this.timer.StartTimer(START_TIMER);
            this.timer.OnSecondPass += this.ChangeSecondText;
            this.timer.OnTimerPass += this.RunGame;
        }

        private void ChangeSecondText(int currentSecond)
        {
            int second = START_TIMER - currentSecond;
            gameStarterView.SetText(second.ToString());
        }
        private void RunGame()
        {
            this.timer.OnSecondPass -= this.ChangeSecondText;
            this.timer.OnTimerPass -= this.RunGame;
            this.timer.OnDestroy();
            this.gameCycle.StartGame();
            gameStarterView.Active(false);
        }

        void IDisposable.Dispose()
        {
            this.startButton.OnButtonClick -= this.StartGame;
        }
    }
}