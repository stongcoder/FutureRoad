using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class RotateAroundTween : Tween
    {
        public Transform pivot;
        public Transform target;
        public float angle;
        private float offset;
        private Vector3 axis;
        float usedAngle;
        public RotateAroundTween(Vector3 axis, Transform target, float angle, float time, TweenUpdateType updateType = TweenUpdateType.Update) :
            base(time, updateType)
        {
            this.axis = axis;
            this.target = target;
            this.angle = angle;
        }
        public RotateAroundTween(Transform pivot, Transform target, float angle, float time, TweenUpdateType updateType = TweenUpdateType.Update) :
            base(time, updateType)
        {
            this.pivot = pivot;
            this.target = target;
            this.angle = angle;
        }
        protected override void _Update()
        {
            base._Update();
            var temp = offset * delta;
            if (usedAngle + offset * delta > angle)
            {
                temp = angle - usedAngle;
            }
            var pt = target.position;
            if (pivot != null)
            {
                pt = pivot.position;
            }
            target.RotateAround(pt, axis, temp);
            usedAngle += temp;

        }
        public override void _Start()
        {
            base._Start();
            if (pivot != null)
            {
                axis = pivot.up;
            }
            offset = angle / duration;
            usedAngle = 0f;
            onComplete += () =>
            {
                var pt = target.position;
                if (pivot != null)
                {
                    pt = pivot.position;
                }
                var temp = usedAngle - angle;
                target.RotateAround(pt, axis, -temp);
            };
        }
    }
}
