using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public abstract class RepeatInteract : LevelInteractBase
    {
        public int interactNum;
        public override void ExecuteInit()
        {
            base.ExecuteInit();
            if (interactNum > 0)
            {
                SetModel(interactNum); 
            }
        }
        public override IEnumerator ItorInteract()
        {
            interactNum++;
            yield break;
        }
        public abstract void SetModel(int num);
    }
}

