using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.Configs;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyStateMachine.EnemyMovementStates
{
    public abstract class EnemyTankMovementState
    {
        protected IAIActionCaller _aiActionCaller;
        protected IStateSwitcher _stateSwitcher;
        protected AIConfig _aiConfig;

        protected float _stateDuration;

        private IAIActionCaller actionCaller;

        public EnemyTankMovementState(IAIActionCaller actionCaller, AIConfig aiConfig)
        {
            _aiActionCaller = actionCaller;
            _aiConfig = aiConfig;
        }

        protected EnemyTankMovementState(IAIActionCaller actionCaller)
        {
            this.actionCaller = actionCaller;
        }

        public EnemyTankMovementState ActivateState(IStateSwitcher stateSwitcher, float stateFixedDuration)
        {
            _stateSwitcher = stateSwitcher;

            if (stateFixedDuration > 0) _stateDuration = stateFixedDuration;

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