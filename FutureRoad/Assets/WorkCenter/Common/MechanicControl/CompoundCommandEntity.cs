using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTween;
namespace MechanicControl
{
    public class CompoundCommandEntity : MonoBehaviour
    {
        [SerializeReference]
        public List<CommandBase> commands = new List<CommandBase>();
        public TweenBase GetTween()
        {
            if (HelperTool.IsCollectionEmpty(commands)) return null;
            var compound = new CompoundTween();
            foreach (var command in commands)
            {
                compound.Add(command.GetTween());
            }
            return compound;
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
