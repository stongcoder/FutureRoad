using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public abstract class SingleInteract : LevelInteractBase
    {
        public override void ExecuteInit()
        {
            base.ExecuteInit();
            if (isDone)
            {
                SetDone();
            }
        }
        public override IEnumerator ItorInteract()
        {
            yield return null;
        }
        protected abstract void SetDone();
    }
}

