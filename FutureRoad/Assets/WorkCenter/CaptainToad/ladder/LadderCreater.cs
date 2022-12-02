using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace LevelTool
{
    public class LadderCreater : CreaterBase
    {
        public int unitNum;
        [SerializeField] int max = 50;
        [SerializeField] GameObject unitPf;
        private void OnValidate()
        {
            if (unitNum > max||unitNum<0) return;
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
            for(int i = 0; i < unitNum; i++)
            {
                if (i >= units.Count)
                {
                    var go = HelperTool.Instantiate(unitPf,transform);
                    units.Add(go);
                }
                var unit = units[i];
                unit.SetActive(true);
                unit.transform.localPosition=new Vector3(0,i*0.5f,0);
            }
        }
    }

}
