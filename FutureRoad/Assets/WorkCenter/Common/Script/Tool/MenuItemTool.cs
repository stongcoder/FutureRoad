#if UNITY_EDITOR
using UnityEditor;

public class MenuItemTool
{
    [MenuItem("Tools/������Դ", false, -1)]
    static void ExportResource()
    {
        UnityEngine.Object[] objects = Selection.objects; //ѡ�е����ж���
        string[] objectsPath = new string[objects.Length]; //�������ѡ�ж����·��
        for (int i = 0; i < objects.Length; i++)
        {
            objectsPath[i] = AssetDatabase.GetAssetPath(objects[i]);
        }
        objectsPath = AssetDatabase.GetDependencies(objectsPath);
        var savePath = EditorUtility.SaveFilePanel("��ѡ��·��", "", "", "unitypackage");
        if (!string.IsNullOrEmpty(savePath)) //���ѡ��ȡ������·��Ϊ��
        {
            AssetDatabase.ExportPackage(objectsPath, savePath);
        }
    }
}
#endif

