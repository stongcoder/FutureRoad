using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class Sequence : TweenBase
    {
        public List<CompoundTween> tweens=new List<CompoundTween> ();
        public int curIdx;
        public TweenBase curTween => tweens[curIdx];
        public int loopNum;
        public int curLoop;
        public Sequence(TweenUpdateType updateType = TweenUpdateType.Update, 
            int loopNum = 1)
        {
            tweens = new List<CompoundTween>();
            curIdx = 0;
            isInit = false;
            this.loopNum = loopNum;
            this.updateType = updateType;
            curLoop = 0;
        }

        public override void _Start()
        {
            if (isInit) return;
            base._Start();
            SetUpdate(updateType);
            if(!isOver)curTween._Start();
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
            if (isOver) return;
            while (curTween.isOver)
            {
                curIdx++;
                if(curIdx >= tweens.Count)
                {
                    curIdx-=tweens.Count;
                    curLoop++;
                    ResetTweens();
                }
                if (isOver) return;
                curTween._Start();
            }
            curTween.Update();
        }
        protected override bool isFinished
        {
            get
            {
                if (tweens.Count == 0) return true;
                if (loopNum == -1) return false;
                return curLoop >= loopNum;
            }
        }
        public void Append(float interval)
        {
            _Append(new IntervalTween(interval));
        }
        public void Append(TweenBase tween)
        {
            _Append(tween);
        }
        public void Append(Action cb)
        {
            _Append(new CallBackTween(cb));            
        }
        public void Join(float interVal)
        {
            _Join(new IntervalTween(interVal));
        }
        public void Join(TweenBase tween)
        {
            _Join(tween);
        }
        public void Join(Action cb)
        {
            _Join(new CallBackTween(cb));
        }
        private void _Append(TweenBase tween)
        {
            if(isInit) return;
            var compoundTween = new CompoundTween(tween);
            tweens.Add(compoundTween);
        }
        private void _Join(TweenBase tween)
        {
            if (isInit) return;
            if(tweens.Count == 0)
            {
                tweens.Add(new CompoundTween());
            }
            var lastTween=tweens[tweens.Count-1];
            lastTween.Add(tween);
        }

        public void SetLoop(int num)
        {
            if (isInit) return;
            loopNum = num;
        }
        public override void Replay()
        {
            base.Replay();
            curLoop = 0;
            curIdx = 0;
            ResetTweens();
        }
        public void ResetTweens()
        {
            foreach(var tween in tweens)
            {
                tween.Replay();
            }
        }
    }
}

