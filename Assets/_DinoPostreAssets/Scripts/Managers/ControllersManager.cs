using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace Dinopostres.Managers
{
    public class ControllersManager : MonoBehaviour
    {
        public enum PlayerActions
        {
            AttackA,AttackB,Pause,Return,Interaction, Movement, Dispacher,none
        }

        public enum InputState
        {
            Start,Perform,Cancel
        }

        private static ControllersManager CM_instance;
        private DinoPostreAction InS_gameActions;

        private Dictionary<PlayerActions, InputAction> dic_ControllerMap;
        public static ControllersManager _instance => CM_instance;

        private void Awake()
        {
            if (CM_instance != null)
                Destroy(this);

            CM_instance = this;
            InS_gameActions = new DinoPostreAction();
            dic_ControllerMap = new Dictionary<PlayerActions, InputAction>()
            {
                {PlayerActions.AttackA,InS_gameActions.DinopostreController.AttackA },
                {PlayerActions.AttackB,InS_gameActions.DinopostreController.AttackB },
                {PlayerActions.Dispacher,InS_gameActions.DinopostreController.Dispacher },
                {PlayerActions.Interaction,InS_gameActions.DinopostreController.Interaction },
                {PlayerActions.Movement,InS_gameActions.DinopostreController.Movement },
                {PlayerActions.Pause,InS_gameActions.DinopostreController.Pause },
                {PlayerActions.Return,InS_gameActions.DinopostreController.Return },
            };
        }
        private void OnEnable()
        {
            InS_gameActions.Enable();
        }
        private void OnDisable()
        {
            InS_gameActions.Disable();
        }

        public void AddComand(PlayerActions _action, InputState _state, Action<InputAction.CallbackContext> _function)
        {
            switch (_state)
            {
                case InputState.Start:
                    dic_ControllerMap[_action].started+= _function;
                    break;
                case InputState.Perform:
                    dic_ControllerMap[_action].performed += _function;
                    break;
                case InputState.Cancel:
                    dic_ControllerMap[_action].canceled += _function;
                    break;
            } 
        }

        public void RemoveComand(PlayerActions _action, InputState _state, Action<InputAction.CallbackContext> _function)
        {
            switch (_state)
            {
                case InputState.Start:
                    dic_ControllerMap[_action].started -= _function;
                    break;
                case InputState.Perform:
                    dic_ControllerMap[_action].performed -= _function;
                    break;
                case InputState.Cancel:
                    dic_ControllerMap[_action].canceled -= _function;
                    break;
            }
        }

        public InputAction ReturnValue(PlayerActions _action)
        {
            return dic_ControllerMap[_action];
        }

    }
}