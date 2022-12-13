using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTween;
namespace MechanicControl
{
    public class SequenceCommandEntity : CommandEntity
    {    
        public override TweenBase GetTween()
        {
            if (HelperTool.IsCollectionEmpty(commands)) return null;
            var seq = new Sequence();
            foreach (var command in commands)
            {
                var tween = command.GetTween();
                if (tween != null)
                {
                    seq.Append(tween);
                }
            }
            return seq;
        }

        public override void DoAction()
        {
            if (HelperTool.IsCollectionEmpty(commands)) return;
            foreach (var command in commands)
            {
                command.DoAction();
            }
        }
    }
}

