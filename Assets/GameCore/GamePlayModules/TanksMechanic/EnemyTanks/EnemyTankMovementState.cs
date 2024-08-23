namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public abstract class EnemyTankMovementState
    {
        protected IAIActionCaller _aiActionCaller;
        protected IStateSwitcher _stateSwitcher;

        public EnemyTankMovementState(IAIActionCaller actionCaller)
        {
            _aiActionCaller = actionCaller;
        }

        public EnemyTankMovementState ActivateState(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;

            OnStateEnabled();

            return this;
        }

        protected virtual void OnStateEnabled()
        {

        }

        public virtual void OnStateCurrenctlyActive()
        {

        }

        protected virtual void OnStateDisabled()
        {

        }
    }
}