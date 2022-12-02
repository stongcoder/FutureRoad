using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class GoActivateCommand : Command
    {
        public List<Transform> showTargets = new List<Transform>();
        public List<Transform> hideTargets=new List<Transform> ();
        public override TweenBase GetTween()
        {
            var tween = new CallBackTween(() =>
            {
                foreach(var target in showTargets)
                {
                    target.gameObject.SetActive(true);
                }
                foreach(var target in hideTargets)
                {
                    target.gameObject.SetActive(false);
                }
            });
            return tween;
        }
        public override void DoAction()
        {
            foreach (var target in showTargets)
            {
                target.gameObject.SetActive(true);
            }
            foreach (var target in hideTargets)
            {
                target.gameObject.SetActive(false);
            }
        }
    }
}
