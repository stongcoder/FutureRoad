using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class FloatTween : Tween
    {
        System.Func<float> getter;
        System.Action<float> setter;
        float endVal;
        public FloatTween(System.Func<float> getter, System.Action<float> setter,float endVal, float time, TweenUpdateType updateType) : base(time, updateType) 
        {
            this.getter = getter;
            this.setter = setter;
            this.endVal = endVal;
        }
        protected override void _Update()
        {
            base._Update();
            var val = getter();
            setter(val);
        }
        public override void _Start()
        {
            base._Start();
            onComplete += () =>
            {
                setter(endVal);
            };
        }
    }
}
