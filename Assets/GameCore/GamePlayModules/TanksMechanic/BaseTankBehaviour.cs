using Assets.CodeUtilities;
using Assets.GameCore.GamePlayModules.Obstacles;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic
{
    public abstract class BaseTankBehaviour : CachedMonoBehaviour
    {
        [SerializeField] protected TankMovement _tankMovement;
        [SerializeField] protected ObstaclesDetector _obstaclesDetector;

        protected virtual void ColideWithObstacle(Collision2D collision, DefaultObstacles obst)
        {

        }
    }
}