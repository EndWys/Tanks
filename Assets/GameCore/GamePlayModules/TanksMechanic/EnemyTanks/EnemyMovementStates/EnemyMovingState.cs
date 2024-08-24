using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyMovementStates
{
    public class EnemyMovingState : EnemyTankMovementState
    {
        private float _timer = 0;

        public EnemyMovingState(IAIActionCaller actionCaller, AIConfig aiConfig) : base(actionCaller, aiConfig)
        {
        }

        protected override void OnStateEnabled()
        {
            _timer = _stateDuration;
        }

        public override void OnStateCurrenctlyActive()
        {
            _aiActionCaller.CallAIAction(ControllAction.MoveForward);

            _timer -= Time.deltaTime;
            if (_timer <= 0) _stateSwitcher.TransitStateTo(EnemyMoventStates.RotateAndMove, _aiConfig.TurnDurationInMotion);
        }
    }
}