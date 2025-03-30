namespace ShootEmUp
{
    public interface IGameListener { } //Marker

    /// <summary>
    /// ��������� �������, ������� ���������� ��� ������ �������� ��������
    /// </summary>
    public interface IGameStartListener : IGameListener
    {
        void OnGameStart();
    };

    /// <summary>
    /// ��������� �������, ������� ���������� ��� ������� �����
    /// </summary>
    public interface IGamePauseListener : IGameListener
    {
        void OnGamePause();
    };

    /// <summary>
    /// ��������� �������, ������� ���������� ��� ������� �����
    /// </summary>
    public interface IGameResumeListener : IGameListener
    {
        void OnGameResume();
    };

    /// <summary>
    /// ��������� �������, ������� ���������� ��� ��������� �������� ��������
    /// </summary>
    public interface IGameFinishListener : IGameListener
    {
        void OnGameFinish();
    };
}