using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyMovementStates;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public enum EnemyMoventStates
    {
        Move,
        Rotate,
    }

    public interface IStateSwitcher
    {
        void TransitStateTo(EnemyMoventStates state);
    }

    public class EnemyTankMovementStateMachine : IStateSwitcher
    {
        private IAIActionCaller _actionCaller;

        private EnemyTankMovementState _currentState;
        private Dictionary<EnemyMoventStates, EnemyTankMovementState> _stateMap = new();

        public EnemyTankMovementStateMachine(IAIActionCaller caller)
        {
            _actionCaller = caller;
        }

        public void Init()
        {
            RegisterState(new EnemyMovingState(_actionCaller), EnemyMoventStates.Move);
            RegisterState(new EnemyRotatingState(_actionCaller), EnemyMoventStates.Rotate);

            TransitStateTo(EnemyMoventStates.Rotate);
        }

        public void TransitStateTo(EnemyMoventStates moventStates)
        {
            Debug.Log("Transit");

            if (_stateMap.TryGetValue(moventStates, out _currentState))
            {
                _currentState = _currentState.ActivateState(this);
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