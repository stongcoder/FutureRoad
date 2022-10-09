using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class RotateTween : Tween
    {
        public Transform trans;
        public Quaternion start;
        public Vector3 offsetAngle;
        public Quaternion endRot;
        int type;//0:offsetAngle,1:endRot
        public RotateTween(Transform trans, Quaternion endRot, float time, TweenUpdateType updateType) : base(time, updateType)
        {
            this.type = 0;
            this.trans = trans;
            this.endRot = endRot;
        }

        public RotateTween(Transform trans, Vector3 angle, float time, TweenUpdateType updateType) : base(time, updateType)
        {
            this.type = 1;
            this.trans = trans;
            this.offsetAngle = angle;
        }
        protected override void _Update()
        {
            base._Update();
            trans.rotation =Quaternion.Slerp(start, endRot, lerpVal);
        }
        public override void _Start()
        {
            base._Start();
            start = trans.rotation;
            if(type == 1)
            {
                endRot = trans.rotation * Quaternion.Euler(offsetAngle);
            }
        }
    }
}
