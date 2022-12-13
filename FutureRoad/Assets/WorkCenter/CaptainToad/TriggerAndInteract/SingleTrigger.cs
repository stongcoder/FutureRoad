using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public class SingleTrigger : LevelTriggerBase
    {
        [DisplayOnly]public bool hasTriggered;
        public override void ExecuteInit()
        {
            base.ExecuteInit();
            if (hasTriggered)
            {
                SetDone();
            }
        }
        public override void TriggerIn(Collider collider)
        {
            if (hasTriggered) return;
            hasTriggered = true;
            base.TriggerIn(collider);
            DoAction();
        }
        public virtual void DoAction() { }
        public virtual void SetDone() { }
    }
}

