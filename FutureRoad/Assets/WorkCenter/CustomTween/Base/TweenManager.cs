using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CustomTween
{
    public class TweenManager : MonoBehaviour
    {
        public static TweenManager Instance;
        public List<TweenBase> updateTweens=new List<TweenBase>();
        public List<TweenBase> fixedUpdateTweens = new List<TweenBase>();
        private void Awake()
        {
            Instance = this;
            updateTweens = new List<TweenBase>();
            fixedUpdateTweens = new List<TweenBase>();
        }
        private void Update()
        {
            List<TweenBase> removes = new List<TweenBase>();
            var tweenNum = updateTweens.Count;
            for (int i = 0; i < tweenNum; i++)
            {
                TweenBase tween = updateTweens[i];
                if (tween.isOver)
                {
                    removes.Add(tween);
                    continue;
                }
                tween?.Update();
            }
            foreach(var tween in removes)
            {
                updateTweens.Remove(tween);
            }
        }
        private void FixedUpdate()
        {
            List<TweenBase> removes = new List<TweenBase>();
            var tweenNum = fixedUpdateTweens.Count;

            for (int i = 0; i < tweenNum; i++)
            {
                TweenBase tween = fixedUpdateTweens[i];
                if (tween.isOver)
                {
                    removes.Add(tween);
                    continue;
                }
                tween?.Update();
            }
            foreach (var tween in removes)
            {
                fixedUpdateTweens.Remove(tween);
            }
        }
        public void AddTween(TweenBase tween)
        {
            switch (tween.updateType)
            {
                case TweenUpdateType.Update:
                    {
                        updateTweens.Add(tween);
                        break;
                    }
                case TweenUpdateType.FixedUpdate:
                    {
                        fixedUpdateTweens.Add(tween);
                        break;
                    }
            }
        }
    }

}
