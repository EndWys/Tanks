using Assets.CodeUtilities;
using Assets.GameCore.GamePlayModules.Obstacles;
using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class ObstaclesDetector : CachedMonoBehaviour
{
    public event Action<Collision2D, DefaultObstacles> OnCollideWithObstacle = delegate { };

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out DefaultObstacles obstacle))
        {
            OnCollideWithObstacle.Invoke(collision, obstacle);
        }
    }
}
