using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMClearSignal : StateMachineBehaviour
{
    public List<string> enterClear = new List<string>();
    public List<string> exitClear=new List<string>();
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(var enter in enterClear)
        {
            animator.ResetTrigger(enter);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (var enter in exitClear)
        {
            animator.ResetTrigger(enter);
        }
    }
}
