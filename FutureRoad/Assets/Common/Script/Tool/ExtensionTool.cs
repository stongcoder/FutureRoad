using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Extension
{
    public static void LocalReset(this Transform t,bool ignoreScale=false)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        if (!ignoreScale)
        {
            t.localScale = Vector3.one;
        }
    }
}
