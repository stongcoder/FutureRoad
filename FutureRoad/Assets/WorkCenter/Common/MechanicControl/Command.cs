using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class CommandBase
    {
        public string label;
        public CommandType commandType;
        public virtual void DoAction()
        {

        }
        public virtual TweenBase GetTween()
        {
            return null;
        }
    }
    [Serializable]
    public abstract class Command : CommandBase
    {
        public float duration;
    }
    public enum CommandType
    {
        Interval = 0,
        Compound = 1,
        Sequence=2,
        Move = 3,
        Rotate = 4,
        RotateTo = 5,
        RotateAround = 6,
        GoActivate=7,
        CameraFov=8,
        Log=999,
    }
}
