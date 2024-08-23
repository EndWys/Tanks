using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyMovementStates
{
    public class EnemyMovingState : EnemyTankMovementState
    {
        private const float ACTION_DEFAULT_DURRATION = 3f;

        private float _timer = 0;


        public EnemyMovingState(IAIActionCaller actionCaller) : base(actionCaller)
        {
        }

        protected override void OnStateEnabled()
        {
            _timer = ACTION_DEFAULT_DURRATION;
        }

        public override void OnStateCurrenctlyActive()
        {
            _aiActionCaller.CallAIAction(ControllAction.MoveForward);

            _timer -= Time.deltaTime;
            if (_timer <= 0) _stateSwitcher.TransitStateTo(EnemyMoventStates.RotateAndMove);
        }
    }
}