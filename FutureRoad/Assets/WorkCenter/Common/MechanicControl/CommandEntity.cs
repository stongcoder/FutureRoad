using CustomTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public abstract class CommandEntity : MonoBehaviour
    {
        [SerializeReference]
        public List<CommandBase> commands = new List<CommandBase>();
        public abstract TweenBase GetTween();
        public abstract void DoAction();
    }
}

