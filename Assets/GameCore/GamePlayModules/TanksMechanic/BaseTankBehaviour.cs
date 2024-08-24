using Assets.CodeUtilities;
using Assets.GameCore.GamePlayModules.Obstacles;
using Assets.GameCore.GameRunningModules;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic
{
    public abstract class BaseTankBehaviour : CachedMonoBehaviour
    {
        [SerializeField] protected TankMovement _tankMovement;
        [SerializeField] protected ObstaclesDetector _obstaclesDetector;

        public abstract void Init(IGameTicker ticker);

        protected virtual void ColideWithObstacle(Collision2D collision, DefaultObstacles obst)
        {

        }
    }
}