using Assets.CodeUtilities;
using System;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    [RequireComponent(typeof(Collider2D))]
    public class TankSpawnPoint : CachedMonoBehaviour
    {
        public bool IsFree => _currentTankID == Guid.Empty;

        private Guid _currentTankID = Guid.Empty;

        public void PrepareTankToSpawn(Guid id)
        {
            _currentTankID = id;
        }

        public void ExitSpawn(Guid id)
        {
            if (_currentTankID != id) return;

            _currentTankID = Guid.Empty;
        }
    }
}