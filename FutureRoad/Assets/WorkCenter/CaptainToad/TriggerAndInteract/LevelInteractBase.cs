using System.Collections;
using UnityEngine;
namespace MechanicControl
{
    public abstract class LevelInteractBase : LevelTriggerBase
    {
        [DisplayOnly]
        public bool isDone;
        [DisplayOnly]
        public bool isInteracting;
        public override void ExecuteInit()
        {
            base.ExecuteInit();
        }
        public override void TriggerIn(Collider collider)
        {
            if (isDone) return;
            base.TriggerIn(collider);
            if (!isInteracting)
            {
                ShowTip();
            }
        }
        public override void TriggerOut(Collider collider)
        {
            if(isDone) return;
            base.TriggerOut(collider);
            if (!isInteracting)
            {
                CloseTip();
            }
        }
        private void Interact()
        {
            StartCoroutine(ItorDoInteract());
        }
        private IEnumerator ItorDoInteract()
        {
            isInteracting = true;
            CloseTip();
            yield return ItorInteract();
            if (!isDone)
            {
                ShowTip();
            }
            isInteracting=false;
        }
        public abstract IEnumerator ItorInteract();
        private void ShowTip()
        {
            LevelManager.Instance.ShowHandleTips(() =>
            {
                Interact();
            });
        }
        private void CloseTip()
        {
            LevelManager.Instance.CloseHandleTips();

        }

    }
}
