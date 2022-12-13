using System.Collections;
using UnityEngine;

namespace MechanicControl
{
    public abstract class LevelTriggerThenInteract : LevelTriggerBase
    {
        [DisplayOnly]
        public bool isDone;
        [DisplayOnly]
        public bool isInteracting;
        protected bool isTriggerEvtDone;
        protected virtual string tipContent =>"interact";
        public override void ExecuteInit()
        {
            base.ExecuteInit();
        }
        public override void TriggerIn(Collider collider)
        {
            if (isDone) return;
            base.TriggerIn(collider);
            if (isTriggerEvtDone)
            {
                if (!isInteracting)
                {
                    ShowTip();
                }
            }
            else
            {
                StartCoroutine(DoTriggerEvt());
            }
        }
        protected IEnumerator DoTriggerEvt()
        {
            yield return ItorTrigger();
            if (isTriggerEvtDone)
            {
                if (!isInteracting)
                {
                    ShowTip();
                }
            }
        }
        protected abstract IEnumerator ItorTrigger();

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
