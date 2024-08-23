using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyMovementStates
{
    public class EnemyMovingState : EnemyTankMovementState
    {
        private float timer = 0;

        public EnemyMovingState(IAIActionCaller actionCaller) : base(actionCaller)
        {
        }

        protected override void OnStateEnabled()
        {
            timer = 3f;
        }

        public override void OnStateCurrenctlyActive()
        {
            _aiActionCaller.CallAIAction(ControllAction.MoveForward);

            timer -= Time.deltaTime;
            if (timer <= 0) _stateSwitcher.TransitStateTo(EnemyMoventStates.Rotate);
        }
    }
}