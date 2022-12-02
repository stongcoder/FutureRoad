using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaterBase : MonoBehaviour
{
    public List<GameObject> units;
    [ContextMenu("clearChildren")]
    public void Run()
    {
        units = new List<GameObject>();
        transform.ClearChildren();
    }
}
