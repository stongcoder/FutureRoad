using UnityEngine.UIElements;
using UnityEditor.UIElements;
using MechanicControl;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using System.Collections;

namespace UnityEditor
{
    [CustomEditor(typeof(SequenceCommandEntity))]
    public class SequenceCommandEntityInspector : Editor
    {
        Editor editor;
        SequenceCommandEntity entity;
        VisualElement inspector;
        public override VisualElement CreateInspectorGUI()
        {
            entity = (target as SequenceCommandEntity);
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

            var openBtn = new Button(() =>
            {
                SequenceCommandEntityEditor.Open(entity);
            });
            openBtn.text = "打开配置面板";
            inspector.Add(openBtn);
            var playBtn = new Button(() =>
            {
                if (Application.isPlaying)
                {
                    entity.GetTween().Start();
                }
            });
            playBtn.text = "运行动画";
            inspector.Add(playBtn);
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
}

