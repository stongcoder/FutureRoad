using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMOnExit : StateMachineBehaviour
{
    public List<string> onExitMessages = new List<string>();
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var msg in onExitMessages)
        {
            animator.gameObject.SendMessageUpwards(msg);
        }
    }    
}
