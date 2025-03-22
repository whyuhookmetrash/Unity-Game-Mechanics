using UnityEngine;

public class GameMonoBehaviour: MonoBehaviour
{
    protected virtual void Awake()
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

    protected virtual void OnDestroy()
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