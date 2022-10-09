using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class FloatTween : Tween
    {
        System.Func<float> getter;
        System.Action<float> setter;
        public FloatTween(System.Func<float> getter, System.Action<float> setter, float time, TweenUpdateType updateType) : base(time, updateType) 
        {
            this.getter = getter;
            this.setter = setter;
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
        }
    }
}
