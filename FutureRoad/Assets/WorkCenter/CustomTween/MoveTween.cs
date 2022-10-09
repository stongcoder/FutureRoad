using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class MoveTween : Tween
    {
        public Transform target;
        public Vector3 start;
        public Vector3 end;
        public MoveTween(Transform target, Vector3 pos, float time, TweenUpdateType updateType) : base(time, updateType)
        {
            this.target = target;
            this.end = pos;
        }
        protected override void _Update()
        {
            base._Update();
            target.position = start + (end - start) * lerpVal;
        }
        public override void _Start()
        {
            base._Start();
            start = target.position;
        }
    }
}
