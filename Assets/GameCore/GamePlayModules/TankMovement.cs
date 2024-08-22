using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Assets.GameCore.GamePlayModules
{
    public class TankMovement : CachedMonoBehaviour
    {
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
            CachedTransform.position += Vector3.up * Time.deltaTime * 20;
        }

        private void MoveBack()
        {
            CachedTransform.position += Vector3.down * Time.deltaTime * 20;
        }

        private void RotateLeft()
        {
            CachedTransform.Rotate(Vector3.forward * Time.deltaTime * 20);
        }

        private void RotateRight()
        {
            CachedTransform.Rotate(Vector3.back * Time.deltaTime * 20);
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