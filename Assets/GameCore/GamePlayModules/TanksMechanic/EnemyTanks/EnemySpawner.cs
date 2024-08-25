using Assets.GameCore.GamePlayModules.Other.PoolingSystem;
using Assets.GameCore.GamePlayModules.TanksMechanic;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks;
using Assets.GameCore.GameRunningModules;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<TankSpawnPoint> _spawnPoints;
    [SerializeField] private GameObject _enemyTankPrefab;

    private Pooling<BaseTankBehaviour> _tanksPool = new(); 

    private IGameTicker _gameTicker;

    private List<BaseTankBehaviour> _activeTanks = new();

    [Inject]
    private void Construct(IGameTicker gameTicker)
    {
        _gameTicker = gameTicker;
    }

    public void Init()
    {
        _tanksPool.Initialize(_enemyTankPrefab);

        TryToRespawnAll();
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

    private bool IsAnyPossibleSpawnPoint()
    {
        return _spawnPoints.Any(point => point.IsFree);
    } 

    private void OnDestroyedTank(BaseTankBehaviour tank)
    {
        _activeTanks.Remove(tank);
        _tanksPool.Release(tank);
        TryToRespawnAll();
    }

    private void TryToRespawnAll()
    {
        if (_activeTanks.Count == 0) StartCoroutine(StarterSpawn());
    }

    private IEnumerator StarterSpawn()
    {
        yield return SpawnEnemy();
        yield return SpawnEnemy();
        yield return SpawnEnemy();
        yield return SpawnEnemy();
    }
}
