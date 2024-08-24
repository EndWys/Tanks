using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.Configs;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyStateMachine;
using Assets.GameCore.GameRunningModules;
using System;
using UnityEngine;

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

    public class EnemyAIController : CachedMonoBehaviour, IAIActionCaller, IAIController
    {
        [SerializeField] private AIConfig _aiConfig;
        [SerializeField] private EnemyTankBehaviour _enemyTank;

        public event Action<ControllAction> InputAction = delegate (ControllAction action) { };
        public IStateSwitcher StateSwitcher => _stateMachine;

        private IGameTicker _ticker;

        private EnemyTankMovementStateMachine _stateMachine;

        public void Init(IGameTicker ticker)
        {
            _ticker = ticker;

            _stateMachine = new(this, _aiConfig);

            _stateMachine.Init();

            _ticker.OnTick += EveryTickAction;

            _enemyTank.Init(this);
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