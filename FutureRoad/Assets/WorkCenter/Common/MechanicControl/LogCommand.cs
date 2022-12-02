using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class LogCommand : Command
    {
        public string log;
        public override TweenBase GetTween()
        {
            var tween = new CallBackTween(() =>
            {
                Debug.Log(log);
            });
            return tween;
        }
        public override void DoAction()
        {
            Debug.Log(log);

        }
    }
}
