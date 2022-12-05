using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteAlways]
public class ProceduralBlockUnit : MonoBehaviour
{
    public Vector3Int key;
    public ProceduralBlockCreater creater;
    public LevelManager mgr;
    public void ChangeKey(Vector3Int key)
    {
        this.key = key;
        gameObject.name = key.ToString();
    }
    //复制出来的块加入creater中
    [ContextMenu("加入creater中")]
    public void AddToCreater()
    {
        if(mgr == null)
        {
            mgr=FindObjectOfType<LevelManager>();
        }
        creater=mgr.creaters[mgr.createrIndex];
        creater.AddExist(this);
    }
    [ContextMenu("从creater中脱离")]
    public void DetachCreater()
    {
        creater.DetachUnit(key);
        creater = null;
    }
    [ContextMenu("手动移动位置")]
    public void Move()
    {
        creater.MoveUnit(this);
    }
}