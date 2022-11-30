using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExportWnd : MonoBehaviour
{
    [MenuItem("Tools/������Դ��")]
    static void ShowWindow()
    {
        UnityEngine.Object[] objects = Selection.objects; //ѡ�е����ж���
        if (objects.Length == 0)
        {
            Debug.LogError("δѡ�е�����Դ");
            return;
        }
        string[] objectsPath = new string[objects.Length]; //�������ѡ�ж����·��
        for (int i = 0; i < objects.Length; i++)
        {
            objectsPath[i] = AssetDatabase.GetAssetPath(objects[i]);
        }
        objectsPath = AssetDatabase.GetDependencies(objectsPath);
        var savePath = EditorUtility.SaveFilePanel("��ѡ��·��", "", "", "unitypackage");
        if (savePath == "") //���ѡ��ȡ������·��Ϊ��
        {
            return;
        }
        AssetDatabase.ExportPackage(objectsPath, savePath);
        Debug.Log("export done");
    }

}