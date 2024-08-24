using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyMovementStates;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public enum EnemyMoventStates
    {
        Move,
        RotateAndMove,
        RotateStationary,
    }

    public interface IStateSwitcher
    {
        AIConfig AIConfig { get; }
        void TransitStateTo(EnemyMoventStates moventStates, float stateDuration);
    }

    public class EnemyTankMovementStateMachine : IStateSwitcher
    {
        public AIConfig AIConfig => _aiConfig;

        private IAIActionCaller _actionCaller;

        private EnemyTankMovementState _currentState;
        private Dictionary<EnemyMoventStates, EnemyTankMovementState> _stateMap = new();

        private AIConfig _aiConfig;

        public EnemyTankMovementStateMachine(IAIActionCaller caller, AIConfig aIConfig)
        {
            _actionCaller = caller;
            _aiConfig = aIConfig;
        }

        public void Init()
        {
            RegisterState(new EnemyMovingState(_actionCaller, _aiConfig), EnemyMoventStates.Move);
            RegisterState(new EnemyRotatingAndMovingState(_actionCaller, _aiConfig), EnemyMoventStates.RotateAndMove);
            RegisterState(new EnemyRotateStationaryState(_actionCaller, _aiConfig), EnemyMoventStates.RotateStationary);

            TransitStateTo(EnemyMoventStates.RotateStationary, 
                Random.Range(_aiConfig.MinStationaryRotationTime, _aiConfig.MaxStationaryRotationTime));
        }

        public void TransitStateTo(EnemyMoventStates moventStates, float stateDuration)
        {
            if (_stateMap.TryGetValue(moventStates, out _currentState))
            {
                _currentState = _currentState.ActivateState(this, stateDuration);
                return;
            }

            Debug.LogError($"There is no registered {moventStates.ToString()} state in {typeof(EnemyTankMovementStateMachine)}");
        }

        public void RegisterState(EnemyTankMovementState state, EnemyMoventStates moventStates)
        {
            _stateMap.Add(moventStates, state);
        }

        public void OnStateActive()
        {
            if (_currentState == null) return;

            _currentState.OnStateCurrenctlyActive();
        }
    }
}