using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public class DoActionTrigger : SingleTrigger
    {
        public CommandEntity entity;
        public override void DoAction()
        {
            base.DoAction();
            entity?.GetTween().Start();
        }
        public override void SetDone()
        {
            base.SetDone();
        }
    }
}

