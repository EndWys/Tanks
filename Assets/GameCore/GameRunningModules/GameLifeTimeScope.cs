using Assets.GameCore.GamePlayModules.PlayerLogic;
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
            builder.RegisterComponent(_playerStarter);
            builder.RegisterComponent(_enemySpawner);

            builder.RegisterEntryPoint<GameTickPoint>();
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}