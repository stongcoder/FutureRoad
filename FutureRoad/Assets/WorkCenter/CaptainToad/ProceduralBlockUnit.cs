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
    //���Ƴ����Ŀ����creater��
    [ContextMenu("����creater��")]
    public void AddToCreater()
    {
        if(mgr == null)
        {
            mgr=FindObjectOfType<LevelManager>();
        }
        creater=mgr.creaters[mgr.createrIndex];
        creater.AddExist(this);
    }
    [ContextMenu("��creater������")]
    public void DetachCreater()
    {
        creater.DetachUnit(key);
        creater = null;
    }
    [ContextMenu("�ֶ��ƶ�λ��")]
    public void Move()
    {
        creater.MoveUnit(this);
    }
}