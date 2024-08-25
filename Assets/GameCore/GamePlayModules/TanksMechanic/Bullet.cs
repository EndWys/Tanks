using Assets.GameCore.GamePlayModules.Other.PoolingSystem;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic
{
    public class Bullet : PoolingObject
    {
        public const int MAX_BULLET_DISTANCE = 30;
        private const float BULLET_SPEED = 0.5f;

        public event Action<Bullet> OnDestroy = delegate { };

        private Vector3 _target;

        public void Init(Vector3 target)
        {
            _target = target;
        }

        public void FlyToTarget()
        {
            StartCoroutine(MoveUntileRechTarget());
        }

        private IEnumerator MoveUntileRechTarget()
        {
            yield return new WaitUntil(MoveStep);

            OnDestroy.Invoke(this);
        }

        private bool MoveStep()
        {
            CachedTransform.position = Vector3.MoveTowards(CachedTransform.position, _target, BULLET_SPEED);

            if ((CachedTransform.position - _target).magnitude <= BULLET_SPEED)
            {
                return true;
            }

            return false;
        }

        public override void OnRelease()
        {
            OnDestroy = delegate { };
            base.OnRelease();
        }
    }
}