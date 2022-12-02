using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProceduralBlockCreater : MonoBehaviour
{
    public Transform container;
    [SerializeField] Vector3Int initSize;
    [SerializeField]BlockDataCollection blockCollection;
    [ContextMenu("Init")]
    public void Init()
    {
        container.ClearChildren();
        blockCollection = new BlockDataCollection();
        blockCollection.Init(this,initSize);        
        blockCollection.UpdateVisibility();
    }

    public void AddUnit(Vector3Int pos)
    {
        blockCollection.Add(pos);
    }
    public void RemoveUnit(Vector3Int pos)
    {
        blockCollection.Remove(pos);
    }
    public void AddExist(ProceduralBlockUnit unit)
    {
        var pos = unit.transform.localPosition;
        if (Mathf.Abs(pos.x % 1)>0.01f||
            Mathf.Abs(pos.y%1)>0.01f||
            Mathf.Abs(pos.z%1)>0.01f)
        {
            Debug.LogError($"位置不是整数num:{pos}{pos.x % 1},{pos.y % 1},{pos.z % 1}");
            return;          
        }
        blockCollection.AddExist(unit);
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
            if (val.isVisible != state)
            {
                if (state)
                {
                    val.GenerateUnit();
                }
                else
                {
                    val.DestroyUnit();
                }
            }
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
        if(Contain(pos)) return;
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
        var data = new BlockData(roundPos, creater);
        data.isVisible = true;
        data.unit = unit;
        blockDatas[roundPos] = data;
        UpdateVisibility();
        Debug.Log("加入成功");
    }
}
[System.Serializable]
public class BlockData
{
    public Vector3Int pos;
    public bool isVisible;
    public ProceduralBlockCreater creater;
    public ProceduralBlockUnit unit;
    public BlockData(Vector3Int pos, ProceduralBlockCreater creater)
    {
        this.isVisible = false;
        this.unit = null;
        this.creater = creater;

        this.pos = pos;
    }
    public BlockData Copy()
    {
        var data= new BlockData(this.pos,null);
        data.unit = this.unit;
        data.isVisible = this.isVisible;
        data.creater = this.creater;
        return data;
    }
    public void GenerateUnit()
    {
        isVisible = true;
        var go = HelperTool.Instantiate(ProceduralBlockUnit.UnitPrefab, creater.container);
        go.transform.localPosition = pos;
        unit= go.GetComponent<ProceduralBlockUnit>();
        unit.creater = creater;
        unit.ChangeKey(pos);
    }
    public void DestroyUnit()
    {
        isVisible=false;
        if (unit.gameObject != null)
        {
            GameObject.DestroyImmediate(unit.gameObject);
        }
        unit = null;    
    }
}