using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.PlayerLogic;
using Assets.GameCore.GamePlayModules.TanksMechanic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.GameCore.GameRunningModules
{
    public class GameEntryPoint : IStartable
    {
        private PlayerStarter _playerStarter;
        private PlayerTankControlInput _playerInput;

        private EnemySpawner _enemySpawner;

        [Inject]
        public GameEntryPoint(PlayerTankControlInput playerInput, PlayerStarter playerStarter,
            EnemySpawner enemySpawner)
        {
            _playerInput = playerInput;
            _playerStarter = playerStarter;

            _enemySpawner = enemySpawner;
        }


        public void Start()
        {
            _playerInput.Init();
            _playerStarter.Init();

            _enemySpawner.Init();
        }
    }
}