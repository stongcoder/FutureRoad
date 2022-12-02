using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class CameraFovCommand : Command
    {
        public Camera cam;
        public float fov;
        public override TweenBase GetTween()
        {
            TweenBase tween;
            if(duration <= 0)
            {
                tween = new CallBackTween(() =>
                {
                    cam.fieldOfView = fov;
                });
            }
            else
            {
                var step = (fov -cam.fieldOfView) / duration * Time.deltaTime;

                tween = CustomTweenHelper.TweenFloat(() =>
                {
                    return cam.fieldOfView;
                }, (val) =>
                {
                    val = cam.fieldOfView + step;
                    cam.fieldOfView = val;
                },fov, duration);
            }
            return tween;
        }
        public override void DoAction()
        {
            cam.fieldOfView = fov;
        }
    }
}
