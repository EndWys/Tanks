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

    [Inject]
    private void Construct(IGameTicker gameTicker)
    {
        _gameTicker = gameTicker;
    }

    public void Init()
    {
        _tanksPool.Initialize(_enemyTankPrefab);

        StartCoroutine(StarterSpawn());
    }

    private IEnumerator StarterSpawn()
    {
        yield return SpawnEnemy();
        yield return SpawnEnemy();
        yield return SpawnEnemy();
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitUntil(TryToChooseSpawnpoint);

        var possibleSpawnPoint = _spawnPoints.Where(point => point.IsFree).ToList();

        int randIndex = Random.Range(0, possibleSpawnPoint.Count);
        var spawn = possibleSpawnPoint[randIndex];

        var tank = _tanksPool.Collect(null, spawn.CachedTransform.position, false);

        tank.OnDestroy += _tanksPool.Release;

        tank.Init(_gameTicker);

        spawn.PrepareTankToSpawn(tank.ID);
    }

    private bool TryToChooseSpawnpoint()
    {
        return _spawnPoints.Any(point => point.IsFree);
    } 
}
