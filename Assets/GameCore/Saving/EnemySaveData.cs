using System;
using UnityEngine;

namespace Assets.GameCore.Saving
{
    [Serializable]
    public class EnemySaveData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public EnemySaveData(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}