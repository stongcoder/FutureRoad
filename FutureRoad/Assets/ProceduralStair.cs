using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralStair : MonoBehaviour
{
    public Transform pivot;
    public Vector3Int size;
    [SerializeField] List<GameObject> units;
    [SerializeField] GameObject boxPf;
    int allNum=>size.x*size.y*size.z;
    private void OnValidate()
    {
        if (allNum <= 0) return;
        if (allNum > 50) return;
        pivot.localScale=size.ToFloat();
        if(HelperTool.IsCollectionEmpty(units))
        {
            units = new List<GameObject>();
        }
        foreach(var unit in units)
        {
            unit.SetActive(false);
        }
        int num = 0;
        int xLimt = size.x;
        int yLimit = size.y;
        int zLimit = size.z;
        for (int i = 0; i < xLimt; i++)
        {
            for (int j = 0; j < yLimit; j++)
            {
                for (int k = 0; k < zLimit; k++)
                {
                    if (num >= units.Count)
                    {
                        var go = HelperTool.Instantiate(boxPf, transform);
                        units.Add(go);
                    }
                    var unit = units[num];
                    unit.SetActive(true);
                    unit.transform.localPosition = new Vector3(i, j, k)+new Vector3(0.5f,0.5f,0.5f);
                    unit.transform.GetComponent<TransformEditHelper>().physicCheckCenter = transform;
                    num++;
                }
            }
        }
    }
}
