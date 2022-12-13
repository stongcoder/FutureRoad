using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    [ExecuteAlways]
    public class SyncTransform : MonoBehaviour
    {
        public Transform target;
        private void Update()
        {
            if (target == null) return;
            target.position = transform.position;
            target.rotation = transform.rotation;
        }
    }
}
