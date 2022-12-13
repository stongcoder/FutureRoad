using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class MoveCommand : Command
    {
        public MoveCommand()
        {

        }
        public Transform target;
        public Transform endTarget;
        public bool isPlayer;
        public override TweenBase GetTween()
        {
            if (isPlayer)
            {
                target = LevelManager.Instance.player.transform;
            }
            var tween = new MoveTween(target, endTarget, duration);
            return tween;
        }
        public override void DoAction()
        {
            target.position = endTarget.position;
        }
    }
}
