using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public class Switch : DoActionInteract
    {
        public GameObject handle;
        public override IEnumerator ItorInteract()
        {
            handle.SetActive(false);
            yield return base.ItorInteract();
        }
    }

}
