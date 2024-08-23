using Assets.GameCore.GamePlayModules.TanksMechanic;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GameCore.GameInputSystem
{
    //TODO: Make Setting PopUp, or move it to config file
    public static class ControlSettings
    {
        public static Dictionary<ControllAction, KeyCode> InputConfigurations = new()
        {
            {ControllAction.MoveForward, KeyCode.W },
            {ControllAction.MoveBack, KeyCode.S },
            {ControllAction.RotateLeft, KeyCode.A },
            {ControllAction.RotateRight, KeyCode.D },
        };
    }
}