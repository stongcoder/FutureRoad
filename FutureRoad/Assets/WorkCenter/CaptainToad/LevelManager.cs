using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public ProceduralBlockCreater creater => creaters[createrIndex];
    public int createrIndex = 0;
    public List<ProceduralBlockCreater> creaters;
    public GameObject previewObj;
    private void Awake()
    {
        Instance = this;
    }
    public static LevelManager Instance;

    [ContextMenu("´ò¿ª´°¿Ú")]
    public void OpenEditor()
    {
        LevelManagerEditor.Open(this); 
    }
}
