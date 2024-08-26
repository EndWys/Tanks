using System;

namespace Assets.GameCore.Saving
{
    [Serializable]
    public class EnemySpawnerSaveData
    {
        public Guid Id = Guid.Empty;
        public EnemySaveData[] EnemySaveDatas;
        public EnemySpawnerSaveData(EnemySaveData[] savedEnemies, Guid id)
        {
            Id = id;
            EnemySaveDatas = savedEnemies;
        }
    }
}