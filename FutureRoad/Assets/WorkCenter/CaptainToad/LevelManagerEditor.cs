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
        root.Clear();
        var removeTog = new Toggle();
        removeTog.RegisterValueChangedCallback<bool>((evt) =>
        { 
            isRemove = evt.newValue;
        });
        removeTog.value = isRemove;
        removeTog.text = "É¾³ý";
        root.Add(removeTog);
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
        if (Event.current == null) return;
        if (Application.isPlaying) return;
        if(Event.current.type == EventType.KeyDown&&Event.current.keyCode == KeyCode.W)
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
            if (Physics.Raycast(ray, out var hit, 30f,LayerMask.GetMask("ProceduralBlock")))
            {              
                var dir = hit.point - hit.transform.position;
                if (dir.magnitude < 0.1f) return;
                dir = HelperTool.GetNearDir_SixDirIn3D(dir);
                var unit=hit.transform.GetComponentInParent<ProceduralBlockUnit>();
                if (!isRemove)
                {
                    mgr.previewObj.transform.position = hit.point;
                    var pos = unit.transform.localPosition + dir;
                    Vector3Int temp = pos.ToInt();
                    Debug.Log($"pos{hit.transform.position}dir{dir}temp{temp}");
                    mgr.creater.AddUnit(temp);
                }
                else
                {
                    var pos = unit.transform.localPosition;
                    Vector3Int temp = pos.ToInt() ;
                    mgr.creater.RemoveUnit(temp);
                }
            }
        }
    }
}
