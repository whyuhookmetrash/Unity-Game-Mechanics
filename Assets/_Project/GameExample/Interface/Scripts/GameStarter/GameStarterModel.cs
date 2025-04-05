
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
        private Timer timer;

        private readonly StartButtonHandler startButton;
        private readonly GameStarterView gameStarterView;
        private readonly LazyInject<Timer> lazyTimer;
        private readonly GameCycle gameCycle;

        public GameStarterModel(
            StartButtonHandler startButton,
            GameStarterView gameStarterView,
            LazyInject<Timer> lazyTimer,
            GameCycle gameCycle)
        {
            this.startButton = startButton;
            this.gameStarterView = gameStarterView;
            this.lazyTimer = lazyTimer;
            this.gameCycle = gameCycle;
        }

        void IInitializable.Initialize()
        {
            this.startButton.OnButtonClick += this.StartGame;
            this.timer = this.lazyTimer.Value;
        }

        private void StartGame()
        {
            gameStarterView.Active(true);
            this.timer.SetTimer(START_TIMER);
            this.timer.OnSecondPass += this.ChangeSecondText;
            this.timer.OnTimerPass += this.RunGame;
            this.timer.OnEnable();
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
            this.timer.OnDisable();
            this.gameCycle.StartGame();
            gameStarterView.Active(false);
        }

        void IDisposable.Dispose()
        {
            this.startButton.OnButtonClick -= this.StartGame;
            this.timer.OnDestroy();
        }
    }
}