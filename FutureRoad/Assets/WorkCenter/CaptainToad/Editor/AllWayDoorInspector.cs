using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
[CustomEditor(typeof(AllWayDoor))]
public class AllWayDoorInspector : Editor
{
    AllWayDoor door;
    Editor editor;
    VisualElement inspector;
    public override VisualElement CreateInspectorGUI()
    {
        door = (target as AllWayDoor);
        inspector = new VisualElement();
        UnityEngine.Object.DestroyImmediate(editor);
        editor = UnityEditor.Editor.CreateEditor(target);

        IMGUIContainer container = new IMGUIContainer(() =>
        {
            if (editor.target != null)
            {
                editor.OnInspectorGUI();
            }
        });
        inspector.Add(container);
        for (int i = 0; i < 6; i++)
        {
            var index = i;
            var type=(SixDirIn3DType)index;
            var tog = new Toggle(type.ToString());
            tog.RegisterValueChangedCallback((evt) =>
            {
                door.doors[index].SetActive(evt.newValue);
            });
            tog.value=door.doors[index].activeSelf;
            inspector.Add(tog);
        }
        return inspector;
    }
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        List<string> excludes = new List<string>()
            {
                "commands",
            };
        DrawPropertiesExcluding(serializedObject, excludes.ToArray());
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }

}
