using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.Obstacles;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic
{

    public class TankBehaviour : BaseTankBehaviour
    {
        public void Init(IInputSender input)
        {
            _tankMovement.Init(input);
            _obstaclesDetector.OnCollideWithObstacle += ColideWithObstacle;
        }

        protected override void ColideWithObstacle(Collision2D collision, DefaultObstacles obst)
        {
            _tankMovement.Crash(collision.GetContact(0).point, obst.StunDuration, () => { });
        }
    }
}