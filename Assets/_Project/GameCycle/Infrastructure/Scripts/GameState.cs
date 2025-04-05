namespace ShootEmUp
{
    /// <summary>
    /// Основное состояние игры
    /// </summary>
    public enum GameState
    {
        OFF = 0, //Игровой процесс выключен
        PLAY = 1, //Игровой процесс идет
        PAUSE = 2, //Пауза
        FINISH = 3 //Игровой процесс завершен
    }
}
