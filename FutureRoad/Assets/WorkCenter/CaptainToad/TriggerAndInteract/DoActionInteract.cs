using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public class DoActionInteract : LevelInteractBase
    {
        public bool canRepeat;
        public int interactNum;
        public CommandEntity entity;
        public override void ExecuteInit()
        {           
            base.ExecuteInit();
            if (interactNum > 0)
            {
                SetDone(interactNum);
            }
        }
        public override IEnumerator ItorInteract()
        {            
            entity?.GetTween().Start();
            interactNum++;
            if (!canRepeat) isDone = true;
            yield break;
        }
        private void SetDone(int num)
        {
        }
    }
}

