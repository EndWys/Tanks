using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.PlayerLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.GameCore.GamePlayModules
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TankMovement : CachedMonoBehaviour
    {
        [SerializeField] private PlayerMovementSettings _moveSettings;

        private Rigidbody2D _body;

        private IInputSender _input;

        private Dictionary<ControllAction, Action> _tankMoves;

        private bool _isUnderControll = false;

        public void Init(IInputSender input)
        {
            _body = GetComponent<Rigidbody2D>();

            _input = input;

            _tankMoves = BuildTankMovesMap().ToDictionary(x => x.Key, y=> y.Value);

            _input.InputAction += HandleInputAction;

            _isUnderControll = true;
        }

        public void Crash(Vector3 contactPoint, float stunDuration)
        {
            StopCoroutine(LoseControll(0));

            Vector3 repulsionDirection = (contactPoint - CachedTransform.position).normalized;

            _body.AddForce(repulsionDirection * -_moveSettings.CrashForce, ForceMode2D.Impulse);

            StartCoroutine(LoseControll(stunDuration));
        }

        private IEnumerator LoseControll(float stunDuration)
        {
            _isUnderControll = false;
            yield return new WaitForSeconds(stunDuration);
            _isUnderControll = true;
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

        private IEnumerable<KeyValuePair<ControllAction, Action>> BuildTankMovesMap()
        {
            yield return new(ControllAction.MoveForward, MoveForward);
            yield return new(ControllAction.MoveBack, MoveBack);
            yield return new(ControllAction.RotateLeft, RotateLeft);
            yield return new(ControllAction.RotateRight, RotateRight);
        }
    }
}