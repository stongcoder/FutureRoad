using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class MoveCommand : Command
    {
        public MoveCommand()
        {

        }
        public Transform target;
        public Transform endTarget;
        public override TweenBase GetTween()
        {
            var tween = new MoveTween(target, endTarget, duration);
            return tween;
        }
        public override void DoAction()
        {
            target.position = endTarget.position;
        }
    }
}
