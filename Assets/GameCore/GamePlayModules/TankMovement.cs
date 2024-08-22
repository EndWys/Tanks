using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GamePlayModules.PlayerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Assets.GameCore.GamePlayModules
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TankMovement : CachedMonoBehaviour
    {
        [SerializeField] private PlayerMovementSettings _playerMovementSettings;


        private IInputSender _input;

        private Dictionary<ControllAction, Action> _tankMoves;

        [Inject]
        private void Construct(IInputSender input)
        {
            _input = input;
        }

        public void Init()
        {
            _tankMoves = BuildTankMovesMap().ToDictionary(x => x.Key, y=> y.Value);

            _input.InputAction += HandleInputAction;
        }

        private void HandleInputAction(ControllAction actionType)
        {
            _tankMoves[actionType].Invoke();
        }

        private void MoveForward()
        {
            Vector3 direction = CachedTransform.TransformDirection(Vector3.up);

            CachedTransform.position += direction * Time.deltaTime * _playerMovementSettings.MoveSpeed;
        }

        private void MoveBack()
        {
            Vector3 direction = CachedTransform.TransformDirection(Vector3.down);

            CachedTransform.position += direction * Time.deltaTime * _playerMovementSettings.MoveSpeed;
        }

        private void RotateLeft()
        {
            CachedTransform.Rotate(Vector3.forward * Time.deltaTime * _playerMovementSettings.RotateSpeed);
        }

        private void RotateRight()
        {
            CachedTransform.Rotate(Vector3.back * Time.deltaTime * _playerMovementSettings.RotateSpeed);
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