#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomEditorWindowBase : EditorWindow
{
    protected void AddBtn(string name, Action cb, VisualElement parent)
    {
        var button = new Button(cb);
        button.text = name;
        button.style.width = 30;
        parent.Add(button);
    }
    protected void AddListPropertyField(SerializedProperty sp, string name, VisualElement container)
    {
        IMGUIContainer imguiContainer = new IMGUIContainer(() =>
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(sp, new GUIContent(name));
            if (EditorGUI.EndChangeCheck())
                sp.serializedObject.ApplyModifiedProperties();
        });
        container.Add(imguiContainer);
    }
}

#endif
