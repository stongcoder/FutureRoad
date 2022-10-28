using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExportWnd : MonoBehaviour
{
    [MenuItem("Tools/导出资源包")]
    static void ShowWindow()
    {
        UnityEngine.Object[] objects = Selection.objects; //选中的所有对象
        if (objects.Length == 0)
        {
            Debug.LogError("未选中导出资源");
            return;
        }
        string[] objectsPath = new string[objects.Length]; //存放所有选中对象的路径
        for (int i = 0; i < objects.Length; i++)
        {
            objectsPath[i] = AssetDatabase.GetAssetPath(objects[i]);
        }
        objectsPath = AssetDatabase.GetDependencies(objectsPath);
        var savePath = EditorUtility.SaveFilePanel("请选择路径", "", "", "unitypackage");
        if (savePath == "") //如果选择取消，则路径为空
        {
            return;
        }
        AssetDatabase.ExportPackage(objectsPath, savePath);
        Debug.Log("export done");
    }

}