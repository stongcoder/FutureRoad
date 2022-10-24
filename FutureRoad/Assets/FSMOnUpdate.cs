using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnUpdate : StateMachineBehaviour
{
    public List<string> onUpdateMessages = new List<string>();
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(var msg in onUpdateMessages)
        {
            animator.gameObject.SendMessageUpwards(msg);
        }
    }
}
