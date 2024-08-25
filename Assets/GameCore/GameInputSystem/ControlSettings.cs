using Assets.GameCore.GamePlayModules.TanksMechanic;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameCore.GameInputSystem
{
    //TODO: Make Setting PopUp, or move it to config file
    public static class ControlSettings
    {
        public static Dictionary<ControllAction, InputSet> InputConfigurations = new()
        {
            { ControllAction.MoveForward, new(Input.GetKey,KeyCode.W) },
            { ControllAction.MoveBack, new(Input.GetKey,KeyCode.S) },
            { ControllAction.RotateLeft, new(Input.GetKey,KeyCode.A) },
            { ControllAction.RotateRight,new(Input.GetKey,KeyCode.D) },
            { ControllAction.Shoot, new(Input.GetKeyDown,KeyCode.Mouse0) },
        };
    }

    public class InputSet
    {
        public InputSet(Func<KeyCode, bool> input, KeyCode key)
        {
            InputAction = input;
            KeyCode = key;
        }

        public Func<KeyCode, bool> InputAction;
        public KeyCode KeyCode;
    }
}