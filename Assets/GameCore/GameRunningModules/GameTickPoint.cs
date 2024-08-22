using System;
using VContainer.Unity;

namespace Assets.GameCore.GameRunningModules
{
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
        }
    }
}