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
    //复制出来的块加入creater中
    [ContextMenu("加入creater中")]
    public void AddToCreater()
    {
        creater.AddExist(this);
    }
}