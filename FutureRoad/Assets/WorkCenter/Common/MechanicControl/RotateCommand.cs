using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class RotateAroundCommand : Command
    {
        public Transform pivot;
        public List<Transform> targets;
        public float angle;
        public RotateAroundCommand()
        {

        }
        public override TweenBase GetTween()
        {
            var compound = new CompoundTween();
            foreach (var target in targets)
            {
                var tween = new RotateAroundTween(pivot, target, angle, duration);
                compound.Add(tween);
            }
            return compound;
        }
        public override void DoAction()
        {
            foreach (var target in targets)
            {
                target.RotateAround(pivot.position, pivot.up, angle);
            }
        }
    }

    [Serializable]
    public class RotateCommand : Command
    {
        public List<Transform> targets;
        public Vector3 angle;
        public RotateCommand()
        {

        }
        public override TweenBase GetTween()
        {
            var compound = new CompoundTween();
            foreach (var target in targets)
            {
                var tween = new RotateTween(target, angle, duration);
                compound.Add(tween);
            }
            return compound;
        }
        public override void DoAction()
        {
            foreach (var target in targets)
            {
                var tween = new RotateTween(target, angle, duration);
                target.rotation *=  Quaternion.Euler(angle);

            }
        }
    }
    [Serializable]
    public class RotateToCommand : Command
    {
        public List<Transform> targets;
        public Vector3 angle;
        public RotateToCommand()
        {
        }
        public override TweenBase GetTween()
        {
            var compound = new CompoundTween();
            foreach (var target in targets)
            {
                var tween = new RotateTween(target, Quaternion.Euler(angle), duration);
                compound.Add(tween);
            }
            return compound;
        }
        public override void DoAction()
        {
            foreach (var target in targets)
            {
                target.rotation=Quaternion.Euler(angle);
            }
        }
    }
}