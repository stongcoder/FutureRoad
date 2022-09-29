using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperTool
{
    #region Vector
    public static bool Approximately(Vector2 v1,Vector2 v2)
    {
        return Mathf.Approximately(v1.x,v2.x) && Mathf.Approximately(v1.y,v2.y);
    }
    public static float Angle360To180(float ang)
    {
        ang = ang % 360f;
        if (ang > 180f)
        {
            ang =ang-360f;
        }
        return ang;
    }
    #endregion
}
