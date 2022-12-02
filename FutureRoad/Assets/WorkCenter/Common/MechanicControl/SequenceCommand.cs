using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CustomTween;
namespace MechanicControl
{
    [Serializable]
    public class SequenceCommand : CommandBase
    {
        public SequenceCommandEntity entity;
        public SequenceCommand()
        {

        }
        public override TweenBase GetTween()
        {
            return entity.GetTween();
        }
        public override void DoAction()
        {
            base.DoAction();
            entity.DoAction();
        }
    }

}
