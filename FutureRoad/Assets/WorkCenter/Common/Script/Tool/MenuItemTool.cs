#if UNITY_EDITOR
using UnityEditor;

public class MenuItemTool
{
    [MenuItem("Tools/导出资源", false, -1)]
    static void ExportResource()
    {
        UnityEngine.Object[] objects = Selection.objects; //选中的所有对象
        string[] objectsPath = new string[objects.Length]; //存放所有选中对象的路径
        for (int i = 0; i < objects.Length; i++)
        {
            objectsPath[i] = AssetDatabase.GetAssetPath(objects[i]);
        }
        objectsPath = AssetDatabase.GetDependencies(objectsPath);
        var savePath = EditorUtility.SaveFilePanel("请选择路径", "", "", "unitypackage");
        if (!string.IsNullOrEmpty(savePath)) //如果选择取消，则路径为空
        {
            AssetDatabase.ExportPackage(objectsPath, savePath);
        }
    }
}
#endif

