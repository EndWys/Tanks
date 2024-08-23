using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.TanksMechanic;
using UnityEngine;
using VContainer;

namespace Assets.GameCore.GamePlayModules.PlayerLogic
{
    public class PlayerStarter : MonoBehaviour
    {
        [SerializeField] private TankBehaviour _playerTank;

        private IInputSender _input;

        [Inject]
        private void Construct(IInputSender input)
        {
            _input = input;
        }

        public void Init()
        {
            _playerTank.Init(_input);
        }
    }
}