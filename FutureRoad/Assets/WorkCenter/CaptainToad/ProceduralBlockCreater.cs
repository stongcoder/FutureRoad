using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ProceduralBlockCreater : MonoBehaviour
{
    public Transform container;
    [SerializeField] Vector3Int initSize;
    [SerializeField]BlockDataCollection blockCollection;
    public GameObject unitPrefab;
    LevelManager mgr;
    private void FindMgr()
    {
        if (mgr == null)
        {
            mgr = GameObject.FindObjectOfType<LevelManager>(true);
        }
    }
    [ContextMenu("Init")]
    public void Init()
    {
        container.ClearChildren();
        blockCollection = new BlockDataCollection();
        blockCollection.Init(this,initSize);        
        blockCollection.UpdateVisibility();
    }
    private bool CheckIsInteger(Vector3 pos)
    {
        if (Mathf.Abs(pos.x % 1f) > 0.01f ||
           Mathf.Abs(pos.y % 1f) > 0.01f ||
           Mathf.Abs(pos.z % 1f) > 0.01f)
        {
            Debug.LogError($"位置不是整数num:{pos}{pos.x % 1},{pos.y % 1},{pos.z % 1}");
            return false;
        }
        return true;
    }
    private void SetDirty()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }
    public void AddUnit(Vector3Int pos)
    {
        blockCollection.Add(pos);
        SetDirty();
    }
    public void RemoveUnit(Vector3Int pos)
    {
        blockCollection.Remove(pos);
        SetDirty();
    }

    public void AddExist(ProceduralBlockUnit unit)
    {
        var pos = unit.transform.localPosition;
        if (!CheckIsInteger(pos))
        {
            return;
        }
        blockCollection.AddExist(unit);
        SetDirty();
    }
    public void MoveUnit(ProceduralBlockUnit unit)
    {
        if (!CheckIsInteger(unit.transform.localPosition))
        {
            return;
        }
        blockCollection.MoveExist(unit);
        SetDirty();
    }

    public void DetachUnit(Vector3Int pos)
    {
        blockCollection.Detach(pos);
        SetDirty();
    }
}

[System.Serializable]
public class BlockDataCollection
{
    public ProceduralBlockCreater creater;
    public CustomDictionary<Vector3Int, BlockData> blockDatas = new CustomDictionary<Vector3Int, BlockData>();
    public void Init(ProceduralBlockCreater creater,Vector3Int initSize)
    {
        this.creater = creater;
        blockDatas = new CustomDictionary<Vector3Int, BlockData>();
        for (int i = 0; i < initSize.x; i++)
        {
            for (int j = 0; j < initSize.y; j++)
            {
                for (int k = 0; k < initSize.z; k++)
                {
                    blockDatas[new Vector3Int(i,j,k)] = new BlockData(new Vector3Int(i, j, k), creater);
                }
            }
        }
        UpdateVisibility();
    }
    public void UpdateVisibility()
    {       
        for(int i = 0; i < blockDatas.keys.Count; i++)
        {
            var key=blockDatas.keys[i];
            var val=blockDatas.vals[i];
            var state = IsVisible(key);
            if(val.unit == null || val.unit.gameObject == null)
            {
                val.GenerateUnit();
            }
            //if (val.isVisible != state)
            //{
            //    if (state)
            //    {
            //        val.GenerateUnit();
            //    }
            //    else
            //    {
            //        val.DestroyUnit();
            //    }
            //}
            val.isVisible = state;
        }
    }
    public bool IsVisible(Vector3Int pos)
    {
        for(int i = 0; i < 6; i++)
        {
            var type = (SixDirIn3DType)i;
            if (!CheckDir(pos, type)) 
            {
                return true;
            } 
        }
        return false;
    }

    public bool CheckDir(Vector3Int pos,SixDirIn3DType dir)
    {
        return CheckDir(pos.x, pos.y, pos.z,dir);
    }
    public bool CheckDir( int x, int y, int z, SixDirIn3DType dir)
    {
        switch (dir)
        {
            case SixDirIn3DType.Forward:
                {
                    z += 1;
                }
                break;
            case SixDirIn3DType.Backward:
                {
                    z -= 1;
                }
                break;
            case SixDirIn3DType.Up:
                {
                    y += 1;
                }
                break;
            case SixDirIn3DType.Down:
                {
                    y -= 1;
                }
                break;
            case SixDirIn3DType.Left:
                {
                    x -= 1;
                }
                break;
            case SixDirIn3DType.Right:
                {
                    x += 1;
                }
                break;
        }
        return blockDatas.ContainsKey(new Vector3Int(x,y,z));
    }
    public bool Contain(Vector3Int pos)
    {
        return blockDatas.ContainsKey(pos);
    }
    public BlockData Get(Vector3Int pos)
    {
        if (Contain(pos))
        {
            return blockDatas[pos]; 
        }
        return null;
    }

    public void Remove(Vector3Int pos)
    {
        if (!blockDatas.ContainsKey(pos)) return;
        blockDatas[pos].DestroyUnit();
        blockDatas.Remove(pos);
        UpdateVisibility();
    }

    public void Add(Vector3Int pos)
    {
        if (Contain(pos)) 
        {
            Debug.Log("位置已存在物体");
            return;
        } 
        var data = new BlockData(pos, creater);
        blockDatas[pos] = data;
        UpdateVisibility();
    }
    public void AddExist(ProceduralBlockUnit unit)
    {
        var roundPos = unit.transform.localPosition.ToInt();
        if (Contain(roundPos)) 
        {
            Debug.LogError("此位置已被占用");
            return;
        }
        unit.ChangeKey(roundPos);
        unit.transform.SetParent(creater.container);
        var data = new BlockData(roundPos, creater);
        data.isVisible = true;
        data.unit = unit;
        blockDatas[roundPos] = data;
        UpdateVisibility();
        Debug.Log("加入成功");
    }
    public void MoveExist(ProceduralBlockUnit unit)
    {
        var roundPos = unit.transform.localPosition.ToInt();
        if (Contain(roundPos))
        {
            Debug.LogError("此位置已被占用");
            unit.gameObject.name = ("overlapped,移动后重新使用move功能");
            return;
        }
        var data = blockDatas[unit.key];
        blockDatas.Remove(unit.key);
        unit.ChangeKey(roundPos);
        blockDatas[roundPos] = data;
        UpdateVisibility();
        Debug.Log("移动成功");
    }

    public void Detach(Vector3Int pos)
    {
        if (!blockDatas.ContainsKey(pos))
        {
            Debug.LogError("该位置不存在block");
        }
        blockDatas.Remove(pos);
        UpdateVisibility();
    }
}
