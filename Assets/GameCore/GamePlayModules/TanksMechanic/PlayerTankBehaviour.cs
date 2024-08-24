using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.Obstacles;
using Assets.GameCore.GameRunningModules;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic
{

    public class PlayerTankBehaviour : BaseTankBehaviour
    {
        private PlayerTankControlInput _inputController;

        public override void Init(IGameTicker ticker)
        {
            _inputController = new();
            _inputController.Init(ticker);
            _tankMovement.Init(_inputController);
            _obstaclesDetector.OnCollideWithObstacle += ColideWithObstacle;
        }

        protected override void ColideWithObstacle(Collision2D collision, DefaultObstacles obst)
        {
            _tankMovement.Crash(collision.GetContact(0).point, obst.StunDuration, () => { });
        }
    }
}