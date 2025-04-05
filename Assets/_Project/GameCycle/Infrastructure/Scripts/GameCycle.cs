using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class GameCycle
    {
        public GameState MainState { get; private set; }

        private readonly List<IGameListener> _gameListeners = new();

        public void AddListener(IGameListener listener)
        {
            _gameListeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            _gameListeners.Remove(listener);
        }

        public void StartGame()
        {
            if (this.MainState != GameState.OFF)
            {
                return;
            }

            this.MainState = GameState.PLAY;

            foreach (var it in _gameListeners)
            {
                if (it is IGameStartListener listener)
                {
                    listener.OnGameStart();
                }
            }
        }

        public void FinishGame()
        {
            if (this.MainState != GameState.PLAY)
            {
                return;
            }

            this.MainState = GameState.FINISH;

            foreach (var it in _gameListeners)
            {
                if (it is IGameFinishListener listener)
                {
                    listener.OnGameFinish();
                }
            }
        }

        public void PauseGame()
        {
            if (this.MainState != GameState.PLAY)
            {
                return;
            }

            this.MainState = GameState.PAUSE;

            foreach (var it in _gameListeners)
            {
                if (it is IGamePauseListener listener)
                {
                    listener.OnGamePause();
                }
            }
        }

        public void ResumeGame()
        {
            if (this.MainState != GameState.PAUSE)
            {
                return;
            }

            this.MainState = GameState.PLAY;

            foreach (var it in _gameListeners)
            {
                if (it is IGameResumeListener listener)
                {
                    listener.OnGameResume();
                }
            }
        }
    }
}
