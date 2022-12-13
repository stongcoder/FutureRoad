using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CaptainToad;
namespace MechanicControl
{
    public abstract class LevelTriggerBase : MonoBehaviour,ILevelLife
    {
        public int ID;
        protected LevelManager levelMgr => LevelManager.Instance;
        protected List<Coroutine> coroutines = new List<Coroutine>();
        [DisplayOnly]public Collider inCol;
        [DisplayOnly] public Collider outCol;
        public virtual void ExecuteInit() { }
        public virtual void ExecuteUpdate() { }
        public virtual void ExecuteFixedUpdate() { }
        public virtual void TriggerIn(Collider collider) 
        { 
            this.inCol = collider;
        }
        public virtual void TriggerOut(Collider collider) 
        {
            this.outCol = collider;
        }
        public virtual void UpdateCb() { }
        protected new void StartCoroutine(IEnumerator itor)
        {
            var coroutine = base.StartCoroutine(itor);
            coroutines.Add(coroutine);
        }
        protected new void StopCoroutine(Coroutine cte)
        {
            if (!coroutines.Contains(cte))
            {
                Debug.LogError("协程未受脚本控制");
                return;
            }
            coroutines.Remove(cte);
            base.StopCoroutine(cte);
        }
        protected virtual void OnDestroy()
        {
            for(int i=0; i < coroutines.Count; i++)
            {
                StopCoroutine(coroutines[i]);
            }
            coroutines.Clear();
        }
    }
    public interface ILevelLife
    {
        public void ExecuteInit();
        public void ExecuteUpdate();
        public void ExecuteFixedUpdate();
    }

}
