using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class LookAtTween : Tween
    {
        public Transform trans;
        public Vector3 endPos;
        public Quaternion startRot;
        public Quaternion endRot;
        public LookAtTween(Transform trans,Vector3 endPos,float duration, TweenUpdateType updateType) : base(duration, updateType)
        {
            this.trans = trans;
            this.endPos = endPos;
        }
        protected override void _Update()
        {
            base._Update();
            trans.rotation = Quaternion.Slerp(startRot,endRot,lerpVal);
        }
        public override void _Start()
        {
            base._Start();
            this.startRot = trans.rotation;
            var forward = (endPos - trans.position).normalized;
            this.endRot = Quaternion.LookRotation(forward, Vector3.up);
        }
    }

}
