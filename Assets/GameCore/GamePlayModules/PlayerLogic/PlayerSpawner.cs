using Assets.GameCore.GamePlayModules.TanksMechanic;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks;
using Assets.GameCore.GameRunningModules;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Assets.GameCore.GamePlayModules.PlayerLogic
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] List<TankSpawnPoint> _spawnPoints;
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
            _playerTank.OnDestroy += delegate 
            {
                StopAllCoroutines();

                StartCoroutine(OnPlayerDestroyed());
            };
        }

        private IEnumerator OnPlayerDestroyed()
        {
            yield return Respawn();
        }

        private IEnumerator Respawn()
        {
            yield return new WaitUntil(IsAnyPossibleSpawnPoint);

            var possibleSpawnPoint = _spawnPoints.Where(point => point.IsFree).ToList();

            int randIndex = Random.Range(0, possibleSpawnPoint.Count);

            TankSpawnPoint spawn = possibleSpawnPoint[randIndex];

            _playerTank.CachedTransform.position = spawn.CachedTransform.position;

            spawn.PrepareTankToSpawn(_playerTank.ID);
        }

        private bool IsAnyPossibleSpawnPoint()
        {
            return _spawnPoints.Any(point => point.IsFree);
        }
    }
}