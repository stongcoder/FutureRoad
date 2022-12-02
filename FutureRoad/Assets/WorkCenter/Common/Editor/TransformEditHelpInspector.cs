//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//[CustomEditor(typeof(TransformEditHelper))]
//public class TransformEditHelpInspector : Editor
//{
//    private void OnSceneGUI()
//    {
//        var helper=target as TransformEditHelper;
//        if (!helper.useHelper)
//        {
//            return;
//        }

//        if (Tools.current == Tool.Move)
//        {
//            Tools.hidden = true;
//            Vector3 movPos = Handles.PositionHandle(helper.transform.position, Quaternion.identity);
//            movPos = ceilPosition(movPos);
//            if (helper.transform.position != movPos)
//            {
//                Undo.RecordObject(helper.transform, "Undo Move");
//            }
//            helper.transform.position = movPos;
//        }

//    }
//    private Vector3 ceilPosition(Vector3 oriPos)
//    {
//        Vector3 pos = new Vector3Int((int)(oriPos.x / 0.25f), (int)(oriPos.y / 0.25f), (int)(oriPos.z / 0.25f)) ;
//        pos *= 0.25f;
//        return pos;
//    }
//}
