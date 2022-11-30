using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionControl : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        //Debug.Log($"{anim.deltaPosition.x},{anim.deltaPosition.y},{anim.deltaPosition.z}");
        SendMessageUpwards("OnUpdateRootMotion",(System.Object)anim.deltaPosition);
    }
}
