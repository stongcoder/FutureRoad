using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        var data = new BlockData(this.pos, null);
        data.unit = this.unit;
        data.isVisible = this.isVisible;
        data.creater = this.creater;
        return data;
    }
    public void GenerateUnit()
    {
        isVisible = true;
        var go = HelperTool.Instantiate(creater.unitPrefab, creater.container);
        go.transform.localPosition = pos;
        unit = go.GetComponent<ProceduralBlockUnit>();
        unit.creater = creater;
        unit.ChangeKey(pos);
    }
    public void DestroyUnit()
    {
        isVisible = false;
        if (unit.gameObject != null)
        {
            GameObject.DestroyImmediate(unit.gameObject);
        }
        unit = null;
    }

}