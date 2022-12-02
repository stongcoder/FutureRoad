using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTween;
namespace MechanicControl
{
    public class SequenceCommandEntity : MonoBehaviour
    {
        [SerializeReference]
        public List<CommandBase> commands = new List<CommandBase>();       
        public TweenBase GetTween()
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

        public void DoAction()
        {
            if (HelperTool.IsCollectionEmpty(commands)) return;
            foreach (var command in commands)
            {
                command.DoAction();
            }
        }
    }
}

