using Assets.CodeUtilities;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.Other
{
    [RequireComponent(typeof(Camera))]
    public class SmoothFollowingCamera : CachedMonoBehaviour
    {
        [SerializeField] private Transform CameraTarget;

        private void Update()
        {
            if (CameraTarget != null)
            {
                Vector3 tp = CameraTarget.position;

                Vector3 cameraPosition = new Vector3(tp.x, tp.y, CachedTransform.position.z);
                CachedTransform.position = cameraPosition;
            }
        }
    }
}