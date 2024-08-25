using Assets.CodeUtilities;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.Other
{
    [RequireComponent(typeof(Camera))]
    public class SmoothFollowingCamera : CachedMonoBehaviour
    {
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private int _cameraSpeed;

        private void Update()
        {
            if (_cameraTarget != null)
            {
                Vector3 tp = _cameraTarget.position;

                Vector3 cameraPosition = new Vector3(tp.x, tp.y, CachedTransform.position.z);

                CachedTransform.position = Vector3.MoveTowards(CachedTransform.position, cameraPosition, _cameraSpeed * Time.deltaTime);
            }
        }
    }
}