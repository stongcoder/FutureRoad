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
        public bool isGlobal;
        int type;//0:offsetAngle,1:endRot
        public RotateTween(Transform trans, Quaternion endRot, float time, bool isGlobal=true, TweenUpdateType updateType=TweenUpdateType.Update) : base(time, updateType)
        {
            this.type = 0;
            this.trans = trans;
            this.endRot = endRot;
            this.isGlobal = isGlobal;
        }

        public RotateTween(Transform trans, Vector3 angle, float time,bool isGlobal=true, TweenUpdateType updateType=TweenUpdateType.Update) : base(time, updateType)
        {
            this.type = 1;
            this.trans = trans;
            this.offsetAngle = angle;
            this.isGlobal=isGlobal;
        }
        protected override void _Update()
        {
            base._Update();
            if (type == 0)
            {
                if (isGlobal)
                {
                    trans.rotation = Quaternion.Slerp(start, endRot, lerpVal);
                }
                else
                {
                    trans.localRotation = Quaternion.Slerp(start, endRot, lerpVal);
                }
            }
            else
            {
                if (isGlobal)
                {
                    trans.rotation = start * Quaternion.Euler(Vector3.Lerp(Vector3.zero, offsetAngle, lerpVal));
                }
                else
                {
                    trans.localRotation=start*Quaternion.Euler(Vector3.Lerp(Vector3.zero,offsetAngle, lerpVal));
                }
            }
            
        }
        public override void _Start()
        {
            base._Start();
            start = isGlobal?trans.rotation:trans.localRotation;
            //if(type == 1)
            //{
            //    endRot = start * Quaternion.Euler(offsetAngle);
            //}
        }
    }
}
