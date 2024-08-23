using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GameRunningModules;
using System;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public interface IAIInputSender : IInputSender { }
    public interface IAIActionCaller
    {
        void CallAIAction(ControllAction actionType);
    }

    public interface IStateSwitcherHolder
    {
        IStateSwitcher StateSwitcher { get; }
    }

    public class EnemyAIController : CachedMonoBehaviour, IAIInputSender, IAIActionCaller
    {
        [SerializeField] private TankBehaviour _enemyTank;

        public event Action<ControllAction> InputAction = delegate (ControllAction action) { };

        private IGameTicker _ticker;

        private EnemyTankMovementStateMachine _stateMachine;


        public void Init(IGameTicker ticker)
        {
            _ticker = ticker;

            _stateMachine = new(this);

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