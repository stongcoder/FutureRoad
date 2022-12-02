using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteAlways]
public class ProceduralBlockUnit : MonoBehaviour
{
    private static GameObject _UnitPrefab;
    public static GameObject UnitPrefab
    {
        get
        {
            if(_UnitPrefab == null)
            {
                _UnitPrefab = Resources.Load<GameObject>("BlockUnit");
            }
            return _UnitPrefab;
        }
    }
    public Vector3Int key;
    public ProceduralBlockCreater creater;
    public void ChangeKey(Vector3Int key)
    {
        this.key = key;
        gameObject.name = key.ToString();
    }
    //���Ƴ����Ŀ����creater��
    [ContextMenu("����creater��")]
    public void AddToCreater()
    {
        creater.AddExist(this);
    }
}