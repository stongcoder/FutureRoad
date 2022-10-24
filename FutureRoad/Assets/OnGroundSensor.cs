using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider col;
    private void FixedUpdate()
    {
        var startPos = transform.position - Vector3.up*0.01f;
        var radius = col.radius;
        var pt1 = startPos + transform.up * radius;
        var pt2= startPos + transform.up*(col.height-radius);

        var cols = Physics.OverlapCapsule(pt1, pt2, radius, LayerMask.GetMask("Ground"));
        if (!HelperTool.IsCollectionEmpty(cols))
        {
            SendMessageUpwards("IsOnGround");

        }
        else
        {
            SendMessageUpwards("IsNotOnGround");
        }
    }
}
