#if UNITY_EDITOR    
using MechanicControl;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
public class LevelManagerEditor : CustomEditorWindowBase
{
    VisualElement root;
    LevelManager mgr;
    bool isRemove;

    public static void Open(LevelManager mgr)
    {
        var wnd = CreateWindow<LevelManagerEditor>();
        try
        {
            wnd.mgr = mgr;
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
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/WorkCenter/CaptainToad/LevelManagerEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(labelFromUXML);
        root = rootVisualElement.Q<ScrollView>();
    }
    public void DrawInspector()
    {
        if (root == null) return;
        if (mgr == null)
        {
            mgr = FindObjectOfType<LevelManager>(true);
        }
        root.Clear();
        var removeTog = new Toggle();
        removeTog.RegisterValueChangedCallback<bool>((evt) =>
        {
            isRemove = evt.newValue;
        });
        removeTog.value = isRemove;
        removeTog.text = "É¾³ý";
        root.Add(removeTog);
        //var so = new SerializedObject(mgr);
        //var creater = so.FindProperty("creater");
        //var pp = new PropertyField(creater);
        //pp.Bind(so); 
        //root.Add(pp);
        List<Toggle> toggles = new List<Toggle>();
        for (int i = 0; i < mgr.creaters.Count; i++)
        {
            var index = i;
            var hcontainer = new VisualElement();
            hcontainer.style.flexDirection = FlexDirection.Row;
            var btn = new Button(() =>
            {
                if (mgr.createrIndex != index)
                {
                    mgr.createrIndex = index;
                }
                for (int j = 0; j < toggles.Count; j++)
                {
                    if (j == index)
                    {
                        toggles[j].value = true;
                    }
                    else
                    {
                        toggles[j].value = false;
                    }
                }
            });
            btn.text = mgr.creaters[index].gameObject.name;
            var tog = new Toggle();
            toggles.Add(tog);
            hcontainer.Add(btn);
            hcontainer.Add(tog);
            root.Add(hcontainer);
        }
        for (int j = 0; j < toggles.Count; j++)
        {
            if (j == mgr.createrIndex)
            {
                toggles[mgr.createrIndex].value = true;
            }
            else
            {
                toggles[mgr.createrIndex].value = false;
            }
        }
    }
    private void OnEnable()
    {
        SceneView.duringSceneGui += CheckInput;
        DrawInspector();
    }
    private void OnDisable()
    {
        SceneView.duringSceneGui -= CheckInput;
    }
    private void CheckInput(SceneView sceneView)
    {
        if (mgr == null)
        {
            mgr = FindObjectOfType<LevelManager>(true);
        }
        if (Event.current == null) return;
        if (Application.isPlaying) return;
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftShift)
        {
            isRemove = !isRemove;
            DrawInspector();
        }
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Space)
        {
            var mousePos = new Vector2(Event.current.mousePosition.x, Screen.height - Event.current.mousePosition.y - 40);
            //Debug.Log(mousePos);
            var ray = sceneView.camera.ScreenPointToRay(mousePos);
            //Debug.DrawLine(ray.origin, ray.origin + ray.direction * 30f, Color.red, 5f);
            if (Physics.Raycast(ray, out var hit, 30f, LayerMask.GetMask("ProceduralBlock")))
            {
                var dir = hit.point - hit.transform.position;
                if (dir.magnitude < 0.1f) return;
                dir = HelperTool.GetNearDir_SixDirIn3D(dir);
                var helper = hit.transform.GetComponentInParent<TransformEditHelper>();
                var localPos = mgr.creater.container.InverseTransformPoint(helper.transform.position);
                if (!isRemove)
                {
                    mgr.previewObj.transform.position = hit.point;
                    var pos = localPos + dir;
                    Vector3Int temp = pos.ToInt();
                    //Debug.Log($"pos{hit.transform.position}dir{dir}temp{temp}");
                    mgr.creater.AddUnit(temp);
                }
                else
                {
                    Vector3Int temp = localPos.ToInt();
                    mgr.creater.RemoveUnit(temp);
                }
            }
            else
            {
                Debug.Log("no proceduralblock");
            }
        }
    }
}

#endif
