using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.Configs;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyStateMachine.EnemyMovementStates
{
    public class EnemyRotatingAndMovingState : EnemyTankMovementState
    {
        private float _timer = 0;
        private ControllAction _direction;

        public EnemyRotatingAndMovingState(IAIActionCaller actionCaller, AIConfig aiConfig) : base(actionCaller, aiConfig)
        {
        }

        protected override void OnStateEnabled()
        {
            _timer = _stateDuration;
            ChooseRandomDirection();
        }

        private void ChooseRandomDirection()
        {
            _direction = ControllAction.RotateLeft;

            int random = Random.Range(0, 2);

            if (random == 1) _direction = ControllAction.RotateRight;
        }

        public override void OnStateCurrenctlyActive()
        {
            _aiActionCaller.CallAIAction(_direction);
            _aiActionCaller.CallAIAction(ControllAction.MoveForward);

            _timer -= Time.deltaTime;
            if (_timer <= 0) _stateSwitcher.TransitStateTo(EnemyMoventStates.Move, _aiConfig.StraightMoveDuration);
        }
    }
}