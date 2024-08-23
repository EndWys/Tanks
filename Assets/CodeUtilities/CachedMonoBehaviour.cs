using UnityEngine;

namespace Assets.CodeUtilities
{
    public class CachedMonoBehaviour : MonoBehaviour
    {
        Transform _cachedTransform;
        GameObject _cachedGameObject;

        public Transform CachedTransform
        {
            get
            {
                if (!_cachedTransform)
                {
                    _cachedTransform = transform;
                }
                return _cachedTransform;
            }
        }

        public GameObject CachedGameObject
        {
            get
            {
                if (!_cachedGameObject)
                {
                    _cachedGameObject = gameObject;
                }
                return _cachedGameObject;
            }
        }
    }
}