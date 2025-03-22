using UnityEngine;

public class GameMonoBehaviour: MonoBehaviour
{
    /* QUESTION:
    Имеет ли место быть такой способ регистрации объектов в реальном времени? 
    */
    private void OnEnable()
    {
        if (this is IGameListener listener)
        {
            GameCycle.Instance.AddListener(listener);
        }
        if (this is ITickableListener tickableListener)
        {
            GameCycle.Instance.AddTickableListener(tickableListener);
        }
    }

    private void OnDisable()
    {
        if (this is IGameListener listener)
        {
            GameCycle.Instance.RemoveListener(listener);
        }
        if (this is ITickableListener tickableListener)
        {
            GameCycle.Instance.RemoveTickableListener(tickableListener);
        }
    }
}