using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks;
using Assets.GameCore.GameRunningModules;
using UnityEngine;
using VContainer;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyTankBehaviour _enemyTankBehaviour;

    private IGameTicker _gameTicker;

    [Inject]
    private void Construct(IGameTicker gameTicker)
    {
        _gameTicker = gameTicker;
    }

    public void Init()
    {
        _enemyTankBehaviour.Init(_gameTicker);
    }
}
