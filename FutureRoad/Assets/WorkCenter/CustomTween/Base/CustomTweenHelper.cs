using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public static class CustomTweenHelper
    {
        public static FloatTween TweenFloat(Func<float>getter,Action<float>setter,float duration,TweenUpdateType updateType = TweenUpdateType.Update)
        {
            var tween=new FloatTween(getter,setter,duration,updateType);
            return tween;
        }
        public static MoveTween TweenMove(this Transform t,Vector3 pos, float duration,TweenUpdateType updateType=TweenUpdateType.Update)
        {
            var moveTween = new MoveTween(t, pos, duration, updateType);
            return moveTween;
        }
        public static RotateTween TweenRotate(this Transform t, Vector3 angle,float duration,TweenUpdateType updateType = TweenUpdateType.Update)
        {
            var rotTween = new RotateTween(t, angle, duration, updateType);
            return rotTween;
        }
        public static RotateTween TweenRotateTo(this Transform t,Quaternion rot,float duration,TweenUpdateType updateType=TweenUpdateType.Update)
        {
            var rotTween = new RotateTween(t, rot, duration, updateType);
            return rotTween;
        }
        public static LookAtTween TweenLookAt(this Transform t,Vector3 endPos,float duration,TweenUpdateType updateType = TweenUpdateType.Update)
        {
            var lookAtTween = new LookAtTween(t, endPos, duration, updateType);
            return lookAtTween;
        }
        public static Sequence DelayDo(Action callBack,float Time)
        {
            var seq=new Sequence();
            seq.Append(Time);
            seq.Append(callBack);
            seq.Start();
            return seq;
        }
        public static Sequence DelayTween(TweenBase tween,float Time)
        {
            var seq = new Sequence();
            seq.Append(tween);
            seq.Start();
            return seq;
        }
        public static Sequence TweenPath(this Transform t, List<Vector3>pts,float duration,TweenUpdateType updateType = TweenUpdateType.Update)
        {
            var seq = new Sequence(TweenUpdateType.FixedUpdate);
            float length = 0;
            for(int i=0; i<pts.Count-1; i++)
            {
                length += Vector3.Distance(pts[i + 1], pts[i]); 
            }
            for(int i=0;i<pts.Count-1; i++)
            {
                var dis= Vector3.Distance(pts[i + 1], pts[i]);
                var time = dis / length * duration;
                seq.Append(t.TweenMove(pts[i + 1], time));
            }
            return seq;
        }
    }
}
