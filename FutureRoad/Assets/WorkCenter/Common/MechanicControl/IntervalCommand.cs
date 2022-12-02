using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class IntervalCommand : Command
    {
        public IntervalCommand()
        {

        }
        public override TweenBase GetTween()
        {
            return new IntervalTween(duration);
        }
        public override void DoAction()
        {
        }
    }
}
