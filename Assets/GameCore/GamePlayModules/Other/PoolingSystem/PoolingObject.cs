using Assets.CodeUtilities;

namespace Assets.GameCore.GamePlayModules.Other.PoolingSystem
{
    public class PoolingObject : CachedMonoBehaviour, IPooling
    {
        public virtual string ObjectName => "";

        public bool IsUsing { get; set; }

        public virtual void OnCollect()
        {
            IsUsing = true;
            CachedGameObject.SetActive(true);
        }

        public virtual void OnRelease()
        {
            IsUsing = false;
            CachedGameObject.SetActive(false);
        }
    }
}