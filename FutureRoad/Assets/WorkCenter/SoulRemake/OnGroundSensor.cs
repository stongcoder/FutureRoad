using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider col;
    //public float yOff;
    //public float rOff;
    public float dirOff;
    private void FixedUpdate()
    {
        //var startPos = transform.position - Vector3.up*yOff;
        //var radius = col.radius+rOff;
        //var pt1 = startPos + transform.up * radius;
        //var pt2= startPos + transform.up*(col.height-radius);
        //var cols = Physics.OverlapCapsule(pt1, pt2, radius, LayerMask.GetMask("Ground"));
        //if (!HelperTool.IsCollectionEmpty(cols))
        //{
        //    SendMessageUpwards("IsOnGround");
        //}
        //else
        //{
        //    SendMessageUpwards("IsNotOnGround");
        //}
        var startPos = transform.position + Vector3.up;
        var flag1 = RayCast(startPos, Vector3.down + Vector3.forward);
        var flag2 = RayCast(startPos, Vector3.down + Vector3.back);
        var flag3 = RayCast(startPos, Vector3.down + Vector3.left);
        var flag4 = RayCast(startPos, Vector3.down + Vector3.right);
        bool flag= flag1||flag2||flag3||flag4;
        if (flag)
        {
            SendMessageUpwards("IsOnGround");
        }
        else
        {
            SendMessageUpwards("IsNotOnGround");
        }
        Debug.DrawRay(startPos, Vector3.down + Vector3.forward* dirOff);
        Debug.DrawRay(startPos, Vector3.down + Vector3.back * dirOff);
        Debug.DrawRay(startPos, Vector3.down + Vector3.left * dirOff);
        Debug.DrawRay(startPos, Vector3.down + Vector3.right * dirOff);

    }
    private bool RayCast(Vector3 point,Vector3 direction,float length =1.5f)
    {
       if(Physics.Raycast(point,direction,length, LayerMask.GetMask("Ground")))
        {
            return true;
        }
       return false;
    }
}
