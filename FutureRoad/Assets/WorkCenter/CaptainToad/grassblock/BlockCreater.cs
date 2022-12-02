using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class BlockCreater : CreaterBase
{
    public Vector3Int factor;
    int allNum=>factor.x*factor.y*factor.z;
    [SerializeField] int xMax = 5;
    [SerializeField] int yMax = 5;
    [SerializeField] int zMax = 5;
    [SerializeField] GameObject gridPf;
    [SerializeField] bool stop;
    private void OnValidate()
    {
        if (stop) return;
        if (allNum < 0) return;
        if (!HelperTool.IsCollectionEmpty(units))
        {
            for(int i = 0; i < units.Count; i++)
            {
                units[i].SetActive(false);
            }
        }
        else
        {
            units = new List<GameObject>();
        }
        int num = 0;
        int xLimt=Mathf.Min(factor.x,xMax);
        int yLimit=Mathf.Min(factor.y,yMax);
        int zLimit=Mathf.Min(factor.z,zMax);
        for(int i = 0; i < xLimt; i++)
        {
            for(int j = 0; j < yLimit; j++)
            {
                for(int k=0; k < zLimit; k++)
                {
                    if (num >= units.Count)
                    {
                        var go=HelperTool.Instantiate(gridPf,transform);
                        units.Add(go);
                    }
                    var unit=units[num];
                    unit.SetActive(true);
                    unit.transform.localScale = new Vector3(factor.x /(float) xLimt, factor.y / (float)yLimit, (float)factor.z / zLimit);
                    unit.transform.localPosition=new Vector3(i*factor.x/(float)xLimt, j*factor.y/(float)yLimit, k*factor.z/(float)zLimit);
                    num++;
                }
            }
        }
        EditorUtility.SetDirty(this);
    }
}
