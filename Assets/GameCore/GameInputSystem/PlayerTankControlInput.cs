using VContainer;

public class PlayerTankControlInput
{
    private IGameTicker _ticker;

    [Inject]
    public PlayerTankControlInput(IGameTicker ticker)
    {
        _ticker = ticker;
    }

    public void Init()
    {

    }
}
