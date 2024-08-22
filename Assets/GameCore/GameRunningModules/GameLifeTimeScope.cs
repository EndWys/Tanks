using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.GameCore.GameRunningModules
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField] private TankMovement _playerTank;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerTankControlInput>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.RegisterComponent(_playerTank);

            builder.RegisterEntryPoint<GameTickPoint>();
            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}