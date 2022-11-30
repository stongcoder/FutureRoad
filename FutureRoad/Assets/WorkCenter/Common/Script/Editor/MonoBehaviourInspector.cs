using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(MonoBehaviour), true)]
public class MonoBehaviourInspector : Editor
{
    readonly string str_Vector3 = typeof(Vector3).ToString();
    readonly string str_Vector3List = typeof(List<Vector3>).ToString();
    List<string> handleStrs
    {
        get
        {
            return new List<string>()
            {
                str_Vector3,
                str_Vector3List,
            };
        }
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        List<string> excludes = new List<string>();
        DrawPropertiesExcluding(serializedObject, excludes.ToArray());
        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
        Type type = target.GetType();
        #region EditorTestAttribute
        {
            MethodInfo methodInfo = null;
            foreach (var m in type.GetMethods())
            {
                if (m.IsDefined(typeof(EditorTestAttribute), true))
                {
                    methodInfo = m;
                    break;
                }
            }
            if (methodInfo != null)
            {
                if (GUILayout.Button("testRun"))
                {
                    var att = methodInfo.GetCustomAttribute(typeof(EditorTestAttribute)) as EditorTestAttribute;
                    var objs = att.objs.ToArray();
                    methodInfo.Invoke(target, objs);
                }
            }
        }
        #endregion
    }
    #region EditorHandle
    private List<FieldInfo> GetHandles()
    {
        Type type = target.GetType();
        List<FieldInfo> handles = new List<FieldInfo>();

        foreach (var f in type.GetFields())
        {
            var field = f;
            if (field.IsDefined(typeof(EditorHandleAttribute), true))
            {
                var value = field.GetValue(target);
                var name = value.GetType().ToString();
                if (handleStrs.Contains(name))
                {
                    handles.Add(field);
                }                
            }
        }
        return handles;
    }
    private bool HaveHandles()
    {
        return !HelperTool.IsCollectionEmpty(GetHandles());
    }
    private void DrawHandles(SceneView sceneView)
    {
        var fields=GetHandles();
        List<FieldInfo> handles = new List<FieldInfo>();
        foreach (var field in fields)
        {
            var name = field.FieldType.ToString();
            if (name == str_Vector3)
            {
                handles.Add((FieldInfo)field);
                var pos =(Vector3) field.GetValue(target);
                var newPos = Handles.PositionHandle(pos, Quaternion.identity);
                field.SetValue(target, newPos);
                Handles.Label(newPos, field.Name);
            }
            else if (name == str_Vector3List)
            {
                var list = (List<Vector3>)field.GetValue(target);
                if (!HelperTool.IsCollectionEmpty(list))
                {
                    var newPos=new List<Vector3>();
                    for (int i = 0; i < list.Count; i++)
                    {
                        Vector3 v = list[i];
                        newPos.Add(Handles.PositionHandle(v, Quaternion.identity));
                        Handles.Label(v, $"{field.Name}[{i}]");
                    }
                    field.SetValue(target,newPos);
                }
            }
        }        
    }
    #endregion

    private void OnEnable()
    {
        if (HaveHandles())
        {
            SceneView.duringSceneGui += DrawHandles;
        }
    }
    private void OnDisable()
    {
        SceneView.duringSceneGui -= DrawHandles;
    }
}
