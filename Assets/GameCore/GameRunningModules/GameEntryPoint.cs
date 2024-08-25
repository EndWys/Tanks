using Assets.GameCore.GamePlayModules.PlayerLogic;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks;
using VContainer;
using VContainer.Unity;

namespace Assets.GameCore.GameRunningModules
{
    public class GameEntryPoint : IStartable
    {
        private PlayerSpawner _playerSpawner;
        private EnemySpawner _enemySpawner;

        [Inject]
        public GameEntryPoint(PlayerSpawner playerStarter,
            EnemySpawner enemySpawner)
        {
            _playerSpawner = playerStarter;

            _enemySpawner = enemySpawner;
        }


        public void Start()
        {
            _playerSpawner.Init();

            _enemySpawner.Init();
        }
    }
}