using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameEntryPoint : IStartable
{
    private PlayerTankControlInput _playerInput;

    [Inject]
    public GameEntryPoint(PlayerTankControlInput playerInput)
    {
        _playerInput = playerInput;
    }


    public void Start()
    {
        Debug.Log("Start");
    }
}
