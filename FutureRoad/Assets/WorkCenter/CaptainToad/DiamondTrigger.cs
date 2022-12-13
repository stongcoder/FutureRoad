using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MechanicControl
{
    public class DiamondTrigger : SingleTrigger
    {
        public List<Animator> anims;
        public override void DoAction()
        {
            base.DoAction();
            foreach(var anim in anims)
            {
                anim.SetTrigger("death");
            }
        }
    }

}
