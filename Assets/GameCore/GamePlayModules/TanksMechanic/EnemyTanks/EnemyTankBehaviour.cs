using Assets.CodeUtilities;
using Assets.GameCore.GamePlayModules.Obstacles;
using Assets.GameCore.GamePlayModules.TanksMechanic;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public class EnemyTankBehaviour : BaseTankBehaviour
    {
        private IStateSwitcher _stateSwitcher;

        public void Init(IAIController aiController)
        {
            _stateSwitcher = aiController.StateSwitcher;

            _tankMovement.Init(aiController);
            _obstaclesDetector.OnCollideWithObstacle += ColideWithObstacle;
        }

        protected override void ColideWithObstacle(Collision2D collision, DefaultObstacles obst)
        {
            _tankMovement.Crash(collision.GetContact(0).point, obst.StunDuration, () =>
            {
                _stateSwitcher.TransitStateTo(EnemyMoventStates.RotateStationary, 
                    Random.Range(_stateSwitcher.AIConfig.MinStationaryRotationTime, _stateSwitcher.AIConfig.MaxStationaryRotationTime));
            });
        }
    }
}