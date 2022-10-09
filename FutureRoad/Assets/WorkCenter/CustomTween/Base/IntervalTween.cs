using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class IntervalTween : Tween
    {
        public IntervalTween(float duration, TweenUpdateType updateType=TweenUpdateType.Update)
            : base(duration, updateType)
        {
        }
    }
}
