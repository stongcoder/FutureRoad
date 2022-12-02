using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public abstract class TweenBase
    {
        public bool isInit;
        public TweenUpdateType updateType;
        public bool isKilled;
        public Action onComplete;
        public bool isPausing;
        protected abstract bool isFinished { get; }
        public virtual void SetUpdate(TweenUpdateType updateType)
        {
            this.updateType = updateType;
        }
        public bool isOver => isKilled || isFinished;
        public void Update()
        {
            if (isOver) return;
            _Update();
            if (isFinished)
            {
                onComplete?.Invoke();
            }
        }
        protected abstract void _Update();
        public virtual void _Start()
        {
            isInit = true;
        }
        public virtual void Kill(bool complete=false)
        {
            if(isOver) return;
            isKilled = true;
            if (complete)
            {
                onComplete?.Invoke();
            }
        }
        public void Start()
        {
            _Start();
            TweenManager.Instance.AddTween(this);
        }
        public virtual void Replay()
        {
            isKilled = false;   
        }
        public virtual void Pause()
        {
            isPausing = true;
        }
        public virtual void Continue()
        {
            isPausing=false;
        }
    }
    public enum TweenUpdateType
    {
        Update,
        FixedUpdate,
    }
}
