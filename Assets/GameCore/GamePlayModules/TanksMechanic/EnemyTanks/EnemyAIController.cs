using Assets.CodeUtilities;
using Assets.GameCore.GameInputSystem;
using Assets.GameCore.GameRunningModules;
using System;
using System.Collections;
using UnityEngine;
using VContainer;

namespace Assets.GameCore.GamePlayModules.TanksMechanic.EnemyTanks
{
    public interface IAIInputSender : IInputSender { }

    public class EnemyAIController : CachedMonoBehaviour, IAIInputSender
    {
        public event Action<ControllAction> InputAction = delegate (ControllAction action) { };

        private IGameTicker _ticker;

        public void Init(IGameTicker ticker)
        {
            _ticker = ticker;

            _ticker.OnTick += EveryTickAction;

            StartCoroutine(ChooseRandomAction());
        }

        private void EveryTickAction()
        {
            //State machine functional that call actions

            if (_rotate)
            {
                InputAction.Invoke(ControllAction.RotateLeft);
            }

            if(_moving)
            {
                InputAction.Invoke(ControllAction.MoveForward);
            }
        }

        //ONLY TEST

        private bool _rotate;
        private bool _moving;
        private IEnumerator ChooseRandomAction()
        {
            _rotate = true;
            _moving = false;

            int random = UnityEngine.Random.Range(1, 8);

            yield return new WaitForSeconds(random);

            _rotate = false;
            _moving = true;

            random = UnityEngine.Random.Range(1, 8);

            yield return new WaitForSeconds(random);

            yield return ChooseRandomAction();
        }
    }
}