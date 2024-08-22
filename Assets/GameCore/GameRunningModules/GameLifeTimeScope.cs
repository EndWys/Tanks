using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerTankControlInput>(Lifetime.Singleton);

        builder.RegisterEntryPoint<GameTickPoint>();
        builder.RegisterEntryPoint<GameEntryPoint>();
    }
}
