using Assets.GameCore.GamePlayModules.Obstacles;
using Assets.GameCore.GamePlayModules.Other.PoolingSystem;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks;
using Assets.GameCore.GameRunningModules;
using System;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic
{
    public abstract class BaseTankBehaviour : PoolingObject
    {
        [SerializeField] protected TankMovement _tankMovement;
        [SerializeField] protected ObstaclesDetector _obstaclesDetector;
        public Guid ID => _id;
        private Guid _id;

        public virtual void Init(IGameTicker ticker)
        {
            _id = Guid.NewGuid();
        }

        protected virtual void ColideWithObstacle(Collision2D collision, DefaultObstacles obst)
        {

        }

        private void OnTriggerExit2D(Collider2D trigger)
        {
            if (trigger.gameObject.TryGetComponent(out TankSpawnPoint spawn))
            {
                spawn.ExitSpawn(_id);
            }
        }
    }
}