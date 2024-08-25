using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.Configs;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyStateMachine;
using Assets.GameCore.GameRunningModules;
using System;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public interface IAIActionCaller
    {
        void CallAIAction(ControllAction actionType);
    }

    public interface IStateSwitcherHolder
    {
        IStateSwitcher StateSwitcher { get; }
    }

    public interface IAIController : IInputSender, IStateSwitcherHolder { }

    public class EnemyAIController : IAIActionCaller, IAIController
    {
        public event Action<ControllAction> InputAction = delegate (ControllAction action) { };
        public IStateSwitcher StateSwitcher => _stateMachine;

        private IGameTicker _ticker;

        private EnemyTankMovementStateMachine _stateMachine;

        public void Init(IGameTicker ticker, AIConfig aiConfig)
        {
            _ticker = ticker;

            _stateMachine = new(this, aiConfig);

            _stateMachine.Init();

            _ticker.OnTick += EveryTickAction;
        }

        private void EveryTickAction()
        {
            _stateMachine.OnStateActive();
        }

        public void CallAIAction(ControllAction actionType)
        {
            InputAction.Invoke(actionType);
        }
    }
}