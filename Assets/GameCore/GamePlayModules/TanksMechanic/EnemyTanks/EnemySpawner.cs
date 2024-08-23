using Assets.GameCore.GamePlayModules.TanksMechanic;
using Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks;
using Assets.GameCore.GameRunningModules;
using UnityEngine;
using VContainer;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyAIController _enemyAIController;


    private IGameTicker _gameTicker;

    [Inject]
    private void Construct(IGameTicker gameTicker)
    {
        _gameTicker = gameTicker;
    }

    public void Init()
    {
        _enemyAIController.Init(_gameTicker);
    }
}
