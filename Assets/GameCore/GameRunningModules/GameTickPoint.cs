using System;
using UnityEngine;
using VContainer.Unity;

public interface IGameTicker
{
    public event Action OnTick;
}

public class GameTickPoint : ITickable, IGameTicker
{
    public event Action OnTick = delegate { };

    public void Tick()
    {
        OnTick.Invoke();
        Debug.Log("Tick");
    }
}
