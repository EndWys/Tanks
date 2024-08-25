using Assets.GameCore.GamePlayModules.TanksMechanic;
using Assets.GameCore.GameRunningModules;
using UnityEngine;
using VContainer;

namespace Assets.GameCore.GamePlayModules.PlayerLogic
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerTankBehaviour _playerTank;

        private IGameTicker _ticker;

        [Inject]
        private void Construct(IGameTicker ticker)
        {
            _ticker = ticker;
        }

        public void Init()
        {
            _playerTank.Init(_ticker);
        }
    }
}