using Assets.GameCore.GamePlayModules.TanksMechanic;
using Assets.GameCore.GameRunningModules;
using System;
using UnityEngine;
using VContainer;

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

        [Inject]
        public PlayerTankControlInput(IGameTicker ticker)
        {
            _ticker = ticker;
        }

        public void Init()
        {
            _ticker.OnTick += EveryTickAction;
        }

        private void EveryTickAction()
        {
            foreach (var actionPair in ControlSettings.InputConfigurations)
            {
                if (Input.GetKey(actionPair.Value))
                {
                    InputAction.Invoke(actionPair.Key);
                }
            }
        }
    }
}