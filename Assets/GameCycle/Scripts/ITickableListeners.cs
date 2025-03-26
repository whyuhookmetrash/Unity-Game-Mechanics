public interface ITickableListener { } //Marker

#region SYSTEM
public interface ITickable : ITickableListener
{
    void Tick(float deltaTime);
}

public interface IFixedTickable : ITickableListener
{
    void FixedTick(float deltaTime);
}

public interface ILateTickable : ITickableListener
{
    void LateTick(float deltaTime);
}
#endregion

#region GAME
public interface IGameTickable : ITickableListener
{
    void Tick(float deltaTime);
}

public interface IGameFixedTickable : ITickableListener
{
    void FixedTick(float deltaTime);
}

public interface IGameLateTickable : ITickableListener
{
    void LateTick(float deltaTime);
}
#endregion
