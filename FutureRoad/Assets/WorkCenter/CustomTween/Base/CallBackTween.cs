using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class CallBackTween : TweenBase
    {
        public Action callback;
        bool _isFinished;
        public CallBackTween(Action cb)
        {
            this.callback = cb;
        }
        protected override bool isFinished => _isFinished;
        public override void _Start()
        {
            base._Start();
            if (isOver) return;
            callback?.Invoke();
            _isFinished = true;
        }
        public override void Replay()
        {
            base.Replay();
            _isFinished = false;
        }
        protected override void _Update() { }
    }

}
