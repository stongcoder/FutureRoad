using MechanicControl;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class CompoundCommandEntityEditor : EditorWindow
{
    public CompoundCommandEntity entity;
    ScrollView scroll;
    VisualElement root;
    List<bool> foldStates = new List<bool>();

    public static void Open(CompoundCommandEntity entity)
    {
        var wnd = CreateWindow<CompoundCommandEntityEditor>();
        wnd.entity = entity;
        wnd.titleContent = new GUIContent(entity.gameObject.name);
        wnd.foldStates = new List<bool>();
        foreach (var command in entity.commands)
        {
            wnd.foldStates.Add(false);
        }
        try
        {
            wnd.DrawInspector();
        }
        catch (Exception e)
        {
            HelperTool.PrintException(e);
            wnd.Close();
        }
    }
    public void CreateGUI()
    {
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/WorkCenter/Common/MechanicControl/CompoundCommandEntityEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(labelFromUXML);
        root=rootVisualElement.Q<ScrollView>();
    }
    private void OnDestroy()
    {

    }
    Vector2 scrollPos;
    EditorCoroutine scrollCoroutine;
    List<EditorCoroutine> coroutines=new List<EditorCoroutine>();
    SerializedObject so;
    SerializedProperty sp;
    void AddBtn(string name, Action cb, VisualElement parent)
    {
        var button = new Button(cb);
        button.text = name;
        button.style.width = 30;
        parent.Add(button);
    }
    void AddListPropertyField(SerializedProperty sp, string name, VisualElement container)
    {
        IMGUIContainer imguiContainer = new IMGUIContainer(() =>
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(sp,new GUIContent(name));
            if(EditorGUI.EndChangeCheck())
                sp.serializedObject.ApplyModifiedProperties();
        });
        container.Add(imguiContainer);
    }
    PropertyField AddPropertyField(SerializedProperty sp, string name)
    {
        var pp = new PropertyField(sp, name);
        pp.Bind(so);
        coroutines.Add(EditorCoroutineUtility.StartCoroutine(RegisterPP(pp), this));
        return pp;
    }
    Foldout AddCustomPropertyField(SerializedProperty sp,CommandBase cmd,string name,bool isRecursive)
    {
        var members=cmd.GetType().GetMembers();
        var fold = new Foldout();
        fold.text = name;
        for (int i = 0; i < members.Length; i++)
        {
            var member = members[members.Length - 1 - i];
            if (member.MemberType != System.Reflection.MemberTypes.Field) continue;
            var memberSp = sp.FindPropertyRelative(member.Name);
            var transformTypes = new List<string>()
            {
                "targets",
                "showTargets",
                "hideTargets",
            };
            if (transformTypes.Contains(member.Name))
            {
                AddListPropertyField(memberSp, member.Name, fold);
            }
            else if (member.Name == "label")
            {
                var ctn = new VisualElement();
                ctn.style.flexDirection = FlexDirection.Row;
                var iptFd = new TextField("标签");
                iptFd.RegisterCallback<ChangeEvent<string>>((evt) =>
                {
                    cmd.SetFiledValue<string>(evt.newValue, "label");
                });
                iptFd.value = cmd.GetFieldValue<string>("label");
                var btn = new Button(() =>
                {
                    DrawInspector();
                });
                btn.text = "apply";
                ctn.Add(iptFd);
                ctn.Add(btn);
                fold.Add(ctn);
            }
            else
            {
                fold.Add(AddPropertyField(memberSp, member.Name)) ;
            }
        }
        return fold;
    }
    List<Foldout> folds = new List<Foldout>();
    private void DrawInspector()
    {
        var inspector = root;
        scroll = inspector as ScrollView;
        scrollPos = scroll.scrollOffset;
        for (int i = 0; i < coroutines.Count; i++)
        {
            var coroutine = coroutines[i];
            EditorCoroutineUtility.StopCoroutine(coroutine);
        }
        coroutines = new List<EditorCoroutine>();
        inspector.Clear();
        var map = new Dictionary<CommandType, System.Type>()
        {
            [CommandType.Interval] = typeof(IntervalCommand),
            [CommandType.Compound] = typeof(CompoundCommand),
            [CommandType.Sequence] = typeof(SequenceCommand),
            [CommandType.Move] = typeof(MoveCommand),
            [CommandType.Rotate] = typeof(RotateCommand),
            [CommandType.RotateTo] = typeof(RotateToCommand),
            [CommandType.RotateAround] = typeof(RotateAroundCommand),
            [CommandType.GoActivate] = typeof(GoActivateCommand),
            [CommandType.CameraFov] = typeof(CameraFovCommand),
            [CommandType.Log] = typeof(LogCommand),
        };
        for (int i = 0; i < entity.commands.Count; i++)
        {
            var index = i;
            if (entity.commands[index].GetType() != map[entity.commands[index].commandType])
            {
                var cmdType = entity.commands[index].commandType;
                entity.commands[index] = Activator.CreateInstance(map[entity.commands[index].commandType]) as CommandBase;
                entity.commands[index].commandType = cmdType;
            }
        }
        so = new SerializedObject(entity);
        sp = so.FindProperty("commands");
        folds = new List<Foldout>();
        for (int i = 0; i < entity.commands.Count; i++)
        {
            var index = i;
            var data = sp.GetArrayElementAtIndex(index);
            var type = entity.commands[index].GetType();
            var name = entity.commands[index].label;
            if (string.IsNullOrEmpty(name))
            {
                name = index.ToString();
            }            
            var fold = AddCustomPropertyField(data, entity.commands[index], name, true);
            folds.Add(fold);
            inspector.Add(fold);
            var container1 = new VisualElement();
            container1.style.flexDirection = FlexDirection.RowReverse;
            AddBtn("-", () =>
            {
                foldStates.RemoveAt(index);
                entity.commands.RemoveAt(index);
                DrawInspector();
            }, container1);
            if (index > 0)
            {
                AddBtn("up", () =>
                {
                    var temp = entity.commands[index - 1];
                    entity.commands[index - 1] = entity.commands[index];
                    entity.commands[index] = temp;
                    var state=foldStates[index];
                    foldStates[index] = foldStates[index - 1];
                    foldStates[index - 1] = state;

                    DrawInspector();
                }, container1);
            }
            if (index < entity.commands.Count - 1)
            {
                AddBtn("down", () =>
                {
                    var temp = entity.commands[index + 1];
                    entity.commands[index + 1] = entity.commands[index];
                    entity.commands[index] = temp;
                    var state = foldStates[index];
                    foldStates[index] = foldStates[index +1];
                    foldStates[index + 1] = state;
                    DrawInspector();
                }, container1);
            }
            inspector.Add(container1);
        }

        var container2 = new VisualElement();
        container2.style.flexDirection = FlexDirection.RowReverse;
        AddBtn("+", () =>
        {
            entity.commands.Add(new CommandBase());
            foldStates.Add(false);
            DrawInspector();
        }, container2);
        inspector.Add(container2);

        var playBtn = new Button(() =>
        {
            if (Application.isPlaying)
            {
                entity.GetTween().Start();
            }
        });
        playBtn.text = "运行动画";
        inspector.Add(playBtn);

        if (scrollCoroutine != null)
        {
            EditorCoroutineUtility.StopCoroutine(scrollCoroutine);
        }
        scrollCoroutine = EditorCoroutineUtility.StartCoroutine(SetScrollView(), this);
    }

    private IEnumerator SetScrollView()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return null;

        }
        scroll.scrollOffset = Vector2.zero;
        scroll.scrollOffset = scrollPos;
        for (int i = 0; i < folds.Count; i++)
        {
            var index = i;
            Foldout fold = folds[index];
            fold.value = foldStates[index];
            fold.RegisterCallback<ChangeEvent<bool>>((evt) =>
            {
                if (foldStates[index] == evt.newValue) return;
                foldStates[index] = evt.newValue;
            });
        }
        

    }
    private IEnumerator RegisterPP(PropertyField pp)
    {
        yield return new WaitUntil(() =>
        {
            return pp.childCount > 0;
        });
        foreach (var item in pp.Query<PropertyField>().ToList())
        {
            item.RegisterValueChangeCallback((evt) =>
            {
                DrawInspector();
            });
        }        
    }
}
