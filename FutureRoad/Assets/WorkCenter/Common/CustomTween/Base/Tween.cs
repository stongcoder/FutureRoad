using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public abstract class Tween : TweenBase
    {
        public float duration;
        public float lerpVal
        {
            get
            {
                var val = usedTime / duration;
                val = Mathf.Clamp(val, 0, 1f);
                return val;
            }
        }
        public float usedTime;
        public float delta
        {
            get
            {
                float d = 0;
                switch (updateType)
                {
                    case TweenUpdateType.Update:
                        {
                            d = Time.deltaTime;
                        }
                        break;
                    case TweenUpdateType.FixedUpdate:
                        {
                            d = Time.fixedDeltaTime;
                        }
                        break;
                }
                return d;
            }
        }
        public Tween(float duration, TweenUpdateType updateType)
        {
            this.duration = duration;
            this.updateType = updateType;
            this.usedTime = 0;
        }
        protected override void _Update()
        {
            usedTime += delta;
        }
        protected override bool isFinished => usedTime >= duration;
        public override void Replay()
        {
            base.Replay();
            usedTime = 0;
        }

    }
}