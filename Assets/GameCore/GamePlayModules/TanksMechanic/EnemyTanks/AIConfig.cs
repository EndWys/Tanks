using UnityEngine;

[CreateAssetMenu()]
public class AIConfig : ScriptableObject
{
    [SerializeField] public float StraightMoveDuration;
    [SerializeField] public float TurnDurationInMotion;
    [Space]
    [SerializeField] public float MinStationaryRotationTime;
    [SerializeField] public float MaxStationaryRotationTime;
}
