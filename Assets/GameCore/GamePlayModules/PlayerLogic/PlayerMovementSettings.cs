using UnityEngine;

namespace Assets.GameCore.GamePlayModules.PlayerLogic
{
    [CreateAssetMenu()]
    public class PlayerMovementSettings : ScriptableObject
    {
        [SerializeField] public int MoveSpeed;
        [SerializeField] public int RotateSpeed;
    }
}