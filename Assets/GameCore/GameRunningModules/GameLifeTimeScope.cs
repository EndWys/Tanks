using Assets.GameCore.GamePlayModules.PlayerLogic;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks;
using Assets.GameCore.Saving;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.GameCore.GameRunningModules
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private PlayerSpawner _playerStarter;
        [SerializeField] private EnemySpawner _enemySpawner;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SavingService>(Lifetime.Singleton);

            builder.RegisterComponent(_playerStarter);
            builder.RegisterComponent(_enemySpawner);

            builder.RegisterEntryPoint<GameTickPoint>();
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}