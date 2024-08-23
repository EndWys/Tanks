using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.Obstacles;
using UnityEngine;
using VContainer;

namespace Assets.GameCore.GamePlayModules
{
    
    public class TankBehaviour : CachedMonoBehaviour
    {
        [SerializeField] private TankMovement _tankMovement;
        [SerializeField] private ObstaclesDetector _obstaclesDetector;

        private IInputSender _input;

        [Inject]
        private void Construct(IInputSender input)
        {
            _input = input;
        }

        public void Init()
        {
            _tankMovement.Init(_input);
            _obstaclesDetector.OnCollideWithObstacle += ColideWithObstacle;
        }

        private void ColideWithObstacle(Collision2D collision, DefaultObstacles obst) 
        {
            _tankMovement.Crash(collision.GetContact(0).point, obst.StunDuration);
        }
    }
}