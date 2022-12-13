using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public class Brick:SingleTrigger
    {
        public GameObject model;
        public override void DoAction()
        {
            base.DoAction();
            model.SetActive(false);
        }
    }
}

