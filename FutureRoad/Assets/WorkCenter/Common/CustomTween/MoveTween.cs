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
        public Transform endTarget;
        public int tweenType = 0;
        public MoveTween(Transform target, Vector3 pos, float time, TweenUpdateType updateType=TweenUpdateType.Update) : base(time, updateType)
        {
            this.target = target;
            this.end = pos;
            tweenType = 0;
        }
        public MoveTween (Transform target,Transform endTarget,float time,TweenUpdateType updateType=TweenUpdateType.Update):base(time,updateType)
        {
            this.target=target;
            this.endTarget=endTarget;
            tweenType = 1;
        }
        protected override void _Update()
        {
            base._Update();
            target.position = start + (end - start) * lerpVal;
        }
        public override void _Start()
        {
            base._Start();
            if(target.GetComponent<Rigidbody>() != null)
            {
                target.GetComponent<Rigidbody>().isKinematic = true;
            }
            start = target.position;
            if(tweenType == 1)
            {
                end = endTarget.position;
            }
            onComplete += () =>
            {
                target.position = end;
                if (target.GetComponent<Rigidbody>() != null)
                {
                    target.GetComponent<Rigidbody>().isKinematic = false;
                }
            };
        }
        
    }
}
