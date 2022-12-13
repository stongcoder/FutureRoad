using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InteractTip : MonoBehaviour
{
    public Action interactCb;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactCb?.Invoke();
        }
    }
}
