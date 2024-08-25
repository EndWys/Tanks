using Assets.GameCore.GamePlayModules.Obstacles;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.Configs;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks.EnemyStateMachine;
using Assets.GameCore.GameRunningModules;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public class EnemyTankBehaviour : BaseTankBehaviour
    {
        [SerializeField] private AIConfig _aiConfig;

        private EnemyAIController _enemyAIController;

        public override void Init(IGameTicker ticker)
        {
            base.Init(ticker);
            _enemyAIController = new();
            _enemyAIController.Init(ticker, _aiConfig);
            _tankMovement.Init(_enemyAIController);
            _obstaclesDetector.OnCollideWithObstacle += ColideWithObstacle;
        }

        protected override void ColideWithObstacle(Collision2D collision, DefaultObstacles obst)
        {
            _tankMovement.Crash(collision.GetContact(0).point, obst.StunDuration, () =>
            {
                _enemyAIController.StateSwitcher.TransitStateTo(EnemyMoventStates.RotateStationary, 
                    Random.Range(_aiConfig.MinStationaryRotationTime, _aiConfig.MaxStationaryRotationTime));
            });
        }
    }
}