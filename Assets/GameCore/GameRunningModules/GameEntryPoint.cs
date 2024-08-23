using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Assets.GameCore.GameRunningModules
{
    public class GameEntryPoint : IStartable
    {
        private TankMovement _playerTank;
        private PlayerTankControlInput _playerInput;

        [Inject]
        public GameEntryPoint(PlayerTankControlInput playerInput, TankMovement playerTank)
        {
            _playerInput = playerInput;
            _playerTank = playerTank;
        }


        public void Start()
        {
            _playerInput.Init();
            _playerTank.Init();

            Debug.Log("Start");
        }
    }
}