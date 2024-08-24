using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.PlayerLogic;
using Assets.GameCore.GamePlayModules.TanksMechanic;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks;
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
            builder.Register<PlayerTankControlInput>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.RegisterComponent(_playerStarter);
            builder.RegisterComponent(_enemySpawner);

            builder.RegisterEntryPoint<GameTickPoint>();
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}