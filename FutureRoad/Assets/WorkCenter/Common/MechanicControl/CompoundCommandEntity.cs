using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTween;
namespace MechanicControl
{
    public class CompoundCommandEntity : CommandEntity
    {       
        public override TweenBase GetTween()
        {
            if (HelperTool.IsCollectionEmpty(commands)) return null;
            var compound = new CompoundTween();
            foreach (var command in commands)
            {
                compound.Add(command.GetTween());
            }
            return compound;
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
