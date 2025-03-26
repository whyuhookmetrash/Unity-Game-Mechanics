using System.Collections.Generic;
using UnityEngine;

public sealed class GameCycle : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private GameState _mainState;

    public GameState MainState { get { return _mainState; } }

    #region INIT LISTENERS
    private readonly List<IGameListener> _gameListeners = new();

    private readonly List<ITickable> _tickableListeners = new();
    private readonly List<IFixedTickable> _fixedTickableListeners = new();
    private readonly List<ILateTickable> _lateTickableListeners = new();

    private readonly List<IGameTickable> _gameTickableListeners = new();
    private readonly List<IGameFixedTickable> _gameFixedTickableListeners = new();
    private readonly List<IGameLateTickable> _gameLateTickableListeners = new();
    #endregion

    public static GameCycle Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
        _mainState = GameState.OFF;
    }

    #region ADD AND REMOVE LISTENERS
    public void AddListener(IGameListener listener)
    {
        _gameListeners.Add(listener);
    }

    public void RemoveListener(IGameListener listener)
    {
        _gameListeners.Remove(listener);
    }

    public void AddTickableListener(ITickableListener listener)
    {
        if (listener is IGameTickable gameTickableListener)
        {
            _gameTickableListeners.Add(gameTickableListener);
        }
        if (listener is IGameFixedTickable gameFixedTickableListener)
        {
            _gameFixedTickableListeners.Add(gameFixedTickableListener);
        }
        if (listener is IGameLateTickable gameLateTickableListener)
        {
            _gameLateTickableListeners.Add(gameLateTickableListener);
        }

        if (listener is ITickable tickableListener)
        {
            _tickableListeners.Add(tickableListener);
        }
        if (listener is IFixedTickable fixedTickableListener)
        {
            _fixedTickableListeners.Add(fixedTickableListener);
        }
        if (listener is ILateTickable lateTickableListener)
        {
            _lateTickableListeners.Add(lateTickableListener);
        }
    }

    public void RemoveTickableListener(ITickableListener listener)
    {
        if (listener is IGameTickable gameTickableListener)
        {
            _gameTickableListeners.Remove(gameTickableListener);
        }
        if (listener is IGameFixedTickable gameFixedTickableListener)
        {
            _gameFixedTickableListeners.Remove(gameFixedTickableListener);
        }
        if (listener is IGameLateTickable gameLateTickableListener)
        {
            _gameLateTickableListeners.Remove(gameLateTickableListener);
        }

        if (listener is ITickable tickableListener)
        {
            _tickableListeners.Remove(tickableListener);
        }
        if (listener is IFixedTickable fixedTickableListener)
        {
            _fixedTickableListeners.Remove(fixedTickableListener);
        }
        if (listener is ILateTickable lateTickableListener)
        {
            _lateTickableListeners.Remove(lateTickableListener);
        }
    }
    #endregion

    #region GAME
    /// <summary>
    /// Старт игры. Когда игра загрузила все объекты и ресурсы и начинается игровой процесс.
    /// Состояние = PLAY;
    /// </summary>
    [Button("Start Game")]
    public void StartGame()
    {
        if (_mainState == GameState.OFF)
        {
            _mainState = GameState.PLAY;

            List<IGameListener> cache = new List<IGameListener>();
            cache.AddRange(_gameListeners);

            foreach (var it in cache)
            {
                if (it is IGameStartListener listener)
                {
                    listener.OnGameStart();
                }
            }
        }
    }
    /// <summary>
    /// Когда во время игрового процесса нужно вызвать паузу.
    /// Состояние = PAUSE;
    /// </summary>
    [Button("Pause Game")]
    public void PauseGame()
    {
        if (_mainState == GameState.PLAY)
        {
            _mainState = GameState.PAUSE;

            foreach (var it in _gameListeners)
            {
                if (it is IGamePauseListener listener)
                {
                    listener.OnGamePause();
                }
            }
        }
    }
    /// <summary>
    /// Когда во время паузы нужно продолжить игру.
    /// Состояние = PLAY;
    /// </summary>
    [Button("Resume Game")]
    public void ResumeGame()
    {
        if (_mainState == GameState.PAUSE)
        {
            _mainState = GameState.PLAY;

            foreach (var it in _gameListeners)
            {
                if (it is IGameResumeListener listener)
                {
                    listener.OnGameResume();
                }
            }
        }
    }
    /// <summary>
    /// Когда нужно полностью завершить игровой процесс.
    /// Состояние = FINISH;
    /// </summary>
    [Button("Finish Game")]
    public void FinishGame()
    {
        if (_mainState == GameState.PLAY)
        {
            _mainState = GameState.FINISH;

            List<IGameListener> cache = new List<IGameListener>();
            cache.AddRange(_gameListeners);

            foreach (var it in cache)
            {
                if (it is IGameFinishListener listener)
                {
                    listener.OnGameFinish();
                }
            }
        }
    }
    #endregion

    #region UPDATES
    private void Update()
    {
        float time = Time.deltaTime;

        if (_mainState == GameState.PLAY)
        {

            foreach (IGameTickable gameTickable in _gameTickableListeners)
            {
                gameTickable.Tick(time);
            }
        }

        foreach (ITickable tickable in _tickableListeners)
        {
            tickable.Tick(time);
        }
    }

    private void FixedUpdate()
    {
        float time = Time.fixedDeltaTime;

        if (_mainState == GameState.PLAY)
        {
            /* QUESTION:
            На сколько правильно делать такой подход (в качестве теста он реализован только здесь), если
            При создании новых объектов в FixedUpdate они в реальном времени регистрируются в GameFixedTicakble и
            появляется ошибка об изменении списка List во время его итерации? Можно ли это сделать удобнее или менее
            ресурсозатратно чем тут?
            */
            List<IGameFixedTickable> cache = new List<IGameFixedTickable>();
            cache.AddRange(_gameFixedTickableListeners);

            foreach (IGameFixedTickable gameFixedTickable in cache)
            {
                gameFixedTickable.FixedTick(time);
            }
        }

        foreach (IFixedTickable fixedTickable in _fixedTickableListeners)
        {
            fixedTickable.FixedTick(time);
        }
    }

    private void LateUpdate()
    {
        float time = Time.deltaTime;

        if (_mainState == GameState.PLAY)
        {
            foreach (IGameLateTickable gameLateTickable in _gameLateTickableListeners)
            {
                gameLateTickable.LateTick(time);
            }
        }

        foreach (ILateTickable lateTickable in _lateTickableListeners)
        {
            lateTickable.LateTick(time);
        }
    }
    #endregion
}
