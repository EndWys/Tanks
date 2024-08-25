using Assets.GameCore.GamePlayModules.TanksMechanic;
using Assets.GameCore.GameRunningModules;
using System;

namespace Assets.GameCore.GameInputSystem
{
    public interface IInputSender
    {
        public event Action<ControllAction> InputAction;
    }

    public class PlayerTankControlInput : IInputSender
    {
        public event Action<ControllAction> InputAction = delegate(ControllAction action) { };

        private IGameTicker _ticker;

        public void Init(IGameTicker ticker)
        {
            _ticker = ticker;
            _ticker.OnTick += EveryTickAction;
        }

        private void EveryTickAction()
        {
            foreach (var actionPair in ControlSettings.InputConfigurations)
            {
                var inputSet = actionPair.Value;
                if (inputSet.InputAction(inputSet.KeyCode))
                {
                    InputAction.Invoke(actionPair.Key);
                }
            }
        }
    }
}