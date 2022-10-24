using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnEnter : StateMachineBehaviour
{
    public List<string> onEnterMessages = new List<string>();
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(var msg in onEnterMessages)
        {
            animator.gameObject.SendMessageUpwards(msg);
        }
    }
}
