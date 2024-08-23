using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyMovementStates
{
    public class EnemyRotatingState : EnemyTankMovementState
    {
        private float timer = 0;

        public EnemyRotatingState(IAIActionCaller actionCaller) : base(actionCaller)
        {
        }


        protected override void OnStateEnabled()
        {
            timer = 1f;
        }

        public override void OnStateCurrenctlyActive()
        {
            _aiActionCaller.CallAIAction(ControllAction.RotateLeft);

            timer -= Time.deltaTime;
            if (timer <= 0) _stateSwitcher.TransitStateTo(EnemyMoventStates.Move);
        }
    }
}