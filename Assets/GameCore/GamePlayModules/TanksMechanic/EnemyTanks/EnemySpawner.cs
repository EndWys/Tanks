using Assets.GameCore.GamePlayModules.Other.PoolingSystem;
using Assets.GameCore.GameRunningModules;
using Assets.GameCore.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public class EnemySpawner : MonoBehaviour
    {
        const int ENEMY_COUNT = 4;

        [SerializeField] private List<TankSpawnPoint> _spawnPoints;
        [SerializeField] private GameObject _enemyTankPrefab;

        private Pooling<BaseTankBehaviour> _tanksPool = new();

        private IGameTicker _gameTicker;

        private List<BaseTankBehaviour> _activeTanks = new();

        private SavingService _savingService;
        private EnemySpawnerSaveData _saveData;

        [Inject]
        private void Construct(IGameTicker gameTicker, SavingService savingService)
        {
            _gameTicker = gameTicker;
            _savingService = savingService;
        }

        public void Init()
        {
            _tanksPool.Initialize(_enemyTankPrefab);

            _saveData = _savingService.Load<EnemySpawnerSaveData>();

            if (_saveData == null)
            {
                TryToRespawnAll();
            }
            else
            {
                LoadEnemyTanks();
            }

            StartCoroutine(SaveEverySecond());
        }

        private void TryToRespawnAll()
        {
            if (_activeTanks.Count == 0) StartCoroutine(StarterSpawn());
        }

        private IEnumerator StarterSpawn()
        {
            for (int i = 0; i < ENEMY_COUNT; i++)
            {
                yield return SpawnEnemy();
            }
        }

        private bool IsAnyPossibleSpawnPoint()
        {
            return _spawnPoints.Any(point => point.IsFree);
        }

        private IEnumerator SpawnEnemy()
        {
            yield return new WaitUntil(IsAnyPossibleSpawnPoint);

            var possibleSpawnPoint = _spawnPoints.Where(point => point.IsFree).ToList();

            int randIndex = Random.Range(0, possibleSpawnPoint.Count);

            TankSpawnPoint spawn = possibleSpawnPoint[randIndex];

            BaseTankBehaviour tank = _tanksPool.Collect(null, spawn.CachedTransform.position, false);

            _activeTanks.Add(tank);

            tank.Init(_gameTicker);
            spawn.PrepareTankToSpawn(tank.ID);

            tank.OnDestroy += OnDestroyedTank;
        }

        private void OnDestroyedTank(BaseTankBehaviour tank)
        {
            _activeTanks.Remove(tank);
            _tanksPool.Release(tank);
            TryToRespawnAll();
        }

        #region SaveLoad

        private void LoadEnemyTanks()
        {
            foreach (var data in _saveData.EnemySaveDatas)
            {
                BaseTankBehaviour tank = _tanksPool.Collect(null, data.Position, false);

                _activeTanks.Add(tank);
                tank.Init(_gameTicker);
                tank.OnDestroy += OnDestroyedTank;
            }
        }

        private void SaveData()
        {
            List<EnemySaveData> enemySaves = new List<EnemySaveData>();

            foreach (var tank in _activeTanks)
            {
                enemySaves.Add(new(tank.CachedTransform.position, tank.CachedTransform.rotation));
            }

            EnemySpawnerSaveData newSaveData = new(enemySaves.ToArray(), Guid.NewGuid());

            _savingService.Save(newSaveData);
        }

        private IEnumerator SaveEverySecond()
        {
            yield return new WaitForSeconds(1f);
            SaveData();
            yield return SaveEverySecond();
        }

        #endregion
    }
}