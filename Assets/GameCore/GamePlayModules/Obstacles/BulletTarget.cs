using Assets.CodeUtilities;
using System;

namespace Assets.GameCore.GamePlayModules.Obstacles
{
    public class BulletTarget : CachedMonoBehaviour
    {
        public event Action OnBulletHit = () => { };
        public void TakeHit()
        {
            OnBulletHit.Invoke();
        }
    }
}