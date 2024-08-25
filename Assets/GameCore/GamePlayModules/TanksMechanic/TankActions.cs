using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.Obstacles;
using Assets.GameCore.GamePlayModules.Other.PoolingSystem;
using Assets.GameCore.GamePlayModules.PlayerLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules.TanksMechanic
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TankActions : CachedMonoBehaviour
    {
        [SerializeField] private TankConfigurations _moveSettings;

        [SerializeField] private GameObject _bulletPrefab;

        private Pooling<Bullet> _bulletPool = new();

        private Rigidbody2D _body;

        private Dictionary<ControllAction, Action> _tankMoves;

        private bool _isUnderControll = false;

        private IInputSender _input;

        public void Init(IInputSender input)
        {
            _body = GetComponent<Rigidbody2D>();

            _tankMoves = BuildTankMovesMap().ToDictionary(x => x.Key, y => y.Value);

            _isUnderControll = true;

            _input = input;
            _input.InputAction += HandleInputAction;

            _bulletPool.Initialize(_bulletPrefab);
        }

        public void Crash(Vector3 contactPoint, float stunDuration, Action onFinish)
        {
            StopCoroutine(LoseControll(stunDuration, onFinish));

            Vector3 repulsionDirection = (contactPoint - CachedTransform.position).normalized;

            _body.AddForce(repulsionDirection * -_moveSettings.CrashForce, ForceMode2D.Impulse);

            StartCoroutine(LoseControll(stunDuration, onFinish));
        }

        private IEnumerator LoseControll(float stunDuration, Action onFinish)
        {
            _isUnderControll = false;
            yield return new WaitForSeconds(stunDuration);
            _isUnderControll = true;
            onFinish.Invoke();
        }

        private void HandleInputAction(ControllAction actionType)
        {
            if (!_isUnderControll) return;

            _tankMoves[actionType].Invoke();
        }

        private void MoveForward()
        {
            Vector3 direction = CachedTransform.TransformDirection(Vector3.up);

            CachedTransform.position += direction * Time.deltaTime * _moveSettings.MoveSpeed;
        }

        private void MoveBack()
        {
            Vector3 direction = CachedTransform.TransformDirection(Vector3.down);

            CachedTransform.position += direction * Time.deltaTime * _moveSettings.MoveSpeed;
        }

        private void RotateLeft()
        {
            CachedTransform.Rotate(Vector3.forward * Time.deltaTime * _moveSettings.RotateSpeed);
        }

        private void RotateRight()
        {
            CachedTransform.Rotate(Vector3.back * Time.deltaTime * _moveSettings.RotateSpeed);
        }

        private void Shoot()
        {
            Bullet bullet = _bulletPool.Collect(null, CachedTransform.position);

            Vector3 direction = CachedTransform.TransformDirection(Vector3.up);

            List<RaycastHit2D> hits = new();

            Physics2D.Raycast(transform.position, direction, new(), hits);

            BulletTarget bulletTarget = null;
            Vector3 bulletTargetPos = transform.position + direction * Bullet.MAX_BULLET_DISTANCE;

            if (TryToFindBulletTarget(hits, out BulletTarget target, out Vector3 hitPoint))
            {
                bulletTarget = target;
                bulletTargetPos = hitPoint;
            }

            bullet.Init(bulletTargetPos);
            bullet.FlyToTarget();

            if (bulletTarget != null)
            {
                bullet.OnDestroy += delegate { bulletTarget.TakeHit(); };
            }

            bullet.OnDestroy += _bulletPool.Release;
        }

        private bool TryToFindBulletTarget(List<RaycastHit2D> hits, out BulletTarget bulletTarget, out Vector3 hitPoint)
        {
            foreach (RaycastHit2D hit in hits)
            {
                var hitObject = hit.collider.gameObject;

                if (hitObject.TryGetComponent(out BulletTarget target))
                {
                    bulletTarget = target;
                    hitPoint = hit.point;
                    return true;
                }
            }

            bulletTarget = null;
            hitPoint = Vector3.zero;
            return false;
        }

        private IEnumerable<KeyValuePair<ControllAction, Action>> BuildTankMovesMap()
        {
            yield return new(ControllAction.MoveForward, MoveForward);
            yield return new(ControllAction.MoveBack, MoveBack);
            yield return new(ControllAction.RotateLeft, RotateLeft);
            yield return new(ControllAction.RotateRight, RotateRight);
            yield return new(ControllAction.Shoot, Shoot);
        }
    }
}