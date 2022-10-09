using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class CompoundTween : TweenBase
    {
        public List<TweenBase> tweens = new List<TweenBase>();
        public CompoundTween() { }
        public override void _Start()
        {
            base._Start();
            foreach(var t in tweens)
            {
                t._Start();
            }
        }
        public CompoundTween(params TweenBase[] tweens)
        {
            foreach(var t in tweens)
            {
                this.tweens.Add(t);
            }
        }
        protected override bool isFinished
        {
            get
            {
                bool flag = true;
                foreach (var tween in tweens)
                {
                    if (!tween.isOver)
                    {
                        flag = false;
                        break;
                    }
                }
                return flag;
            }
        }
        public override void SetUpdate(TweenUpdateType updateType)
        {
            base.SetUpdate(updateType);
            foreach (var tween in tweens)
            {
                tween.SetUpdate(updateType);
            }
        }
        protected override void _Update()
        {
            foreach (var tween in tweens)
            {
                if (!tween.isOver)
                {
                    tween.Update();
                }
            }
        }
        public void Add(TweenBase tween)
        {
            if (isInit) return;
            tweens.Add(tween);
        }
        public void Add(float time)
        {
            if(isInit) return;
            tweens.Add(new IntervalTween(time));
        }
        public override void Replay()
        {
            base.Replay();
            foreach(var tween in tweens)
            {
                tween.Replay();
            }
        }
    }

}

