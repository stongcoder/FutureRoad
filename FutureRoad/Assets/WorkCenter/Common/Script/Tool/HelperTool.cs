using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class HelperTool
{
    public static Dictionary<SixDirIn3DType, Vector3Int> SixDirs = new Dictionary<SixDirIn3DType, Vector3Int>()
    {
        [SixDirIn3DType.Forward] = Vector3Int.forward,
        [SixDirIn3DType.Backward] = Vector3Int.back,
        [SixDirIn3DType.Left] = Vector3Int.left,
        [SixDirIn3DType.Right] = Vector3Int.right,
        [SixDirIn3DType.Up] = Vector3Int.up,
        [SixDirIn3DType.Down] = Vector3Int.down,
    };
    #region Math
    public static float DisPtToLine(Vector3 target, Vector3 p1, Vector3 p2)
    {
        var pt = ProjectPtToLine(target, p1, p2);
        float distance = Vector3.Distance(target, pt);
        return distance;
    }
    public static Vector3 ProjectPtToLine(Vector3 target, Vector3 p1, Vector3 p2)
    {
        //p1->p2的向量
        Vector3 p1_2 = p2 - p1;
        //p1->target向量
        Vector3 p1_target = target - p1;
        //计算投影p1->f
        Vector3 p1f = Vector3.Project(p1_target, p1_2);
        return p1f + p1;
    }
    public static bool IsPtInLine(Vector3 target, Vector3 p1, Vector3 p2)
    {
        var dis = Vector3.Distance(p1, p2);
        var d1 = Vector3.Distance(p1, target);
        var d2 = Vector3.Distance(p2, target);
        return Mathf.Approximately(dis, d1 + d2);
    }
    public static bool Approximately(Vector2 v1, Vector2 v2)
    {
        return Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y);
    }
    public static bool Approximately(Vector3 v1, Vector3 v2)
    {
        return Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y)&&Mathf.Approximately(v1.z,v2.z);
    }
    public static float Angle360To180(float ang)
    {
        ang = ang % 360f;
        if (ang > 180f)
        {
            ang = ang - 360f;
        }
        return ang;
    }
    public static float RoundNum(float num, float round)
    {
        if (round == 0f) return num;
        round = Mathf.Abs(round);
        var sign = Mathf.Sign(num);
        var val = Mathf.Abs(num);
        var times = Mathf.Floor(val / round);
        var res = val % round;
        if (res >= round / 2f)
        {
            res = round;
        }
        else
        {
            res = 0;
        }
        return sign * (round * times + res);
    }
    public static Vector3 RoundVector3(Vector3 v, float round)
    {
        var x = RoundNum(v.x, round);
        var y = RoundNum(v.y, round);
        var z = RoundNum(v.z, round);
        return new Vector3(x, y, z);
    }
    public static int GetRandomSeed()
    {
        return System.Guid.NewGuid().GetHashCode();
    }
    #endregion
    public static int GetEnumNum(Type enumType)
    {
        return Enum.GetNames(enumType).Length;
    }
    public static bool IsCollectionEmpty(ICollection collection)
    {
        if (collection == null || collection.Count == 0)
        {
            return true;
        }
        return false;
    }
    #region  Save&Load
    public static T LoadResource<T>(string path) where T : UnityEngine.Object
    {
        T t;
        var fPath = Path.GetDirectoryName(path);
        var pureName = Path.GetFileNameWithoutExtension(path);
        var newPath = Path.Combine(fPath, pureName);
        t = Resources.Load<T>(newPath);
        if (t == null)
        {
            Debug.LogError($"资源未找到{path}");
        }
        return t;
    }
    public static bool ContainResource(string path)
    {
        var fPath = Path.GetDirectoryName(path);
        var pureName = Path.GetFileNameWithoutExtension(path);
        var newPath = Path.Combine(fPath, pureName);
        var t = Resources.Load(newPath);
        return t != null;
    }
    public static object LoadFromJson(string path, Type T)
    {
        string json = LoadFile(path);
        var data = JsonUtility.FromJson(json, T);
        return data;
    }
    public static void SaveToJson(object data, string path)
    {
#if UNITY_EDITOR
        string json = "";
        try
        {
            json = JsonUtility.ToJson(data);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return;
        }
        SaveToFile(json, path);
#endif
    }
    public static void SaveToFile(string s, string path)
    {

#if UNITY_EDITOR
        FileInfo fileInfo = new FileInfo(path);
        if (fileInfo != null)
        {
            fileInfo.Delete();
        }
        StreamWriter sw = new StreamWriter(path);
        sw.Write(s);
        sw.Close();
        AssetDatabase.Refresh();
#endif

    }
    public static string LoadFile(string path)
    {
        string s = "";
        StreamReader sw = new StreamReader(path);
        s = sw.ReadToEnd();
        sw.Close();
        return s;
    }
    #endregion
    public static void PrintException(Exception e, string info = "")
    {

#if UNITY_EDITOR
        string s = "";
        if (!string.IsNullOrEmpty(info))
        {
            s += info + "\n";
        }
        if (e.InnerException != null)
        {
            s += "内部异常：\n";
            s += "错误提示：" + e.InnerException.Message;
            s += "空间名：" + e.InnerException.Source + "；" + '\n' +
                                      "方法名：" + e.InnerException.TargetSite + '\n' +
                                      "故障点：" + e.InnerException.StackTrace + '\n';
        }
        s += "错误提示：" + e.Message;
        s += "空间名：" + e.Source + "；" + '\n' +
                                  "方法名：" + e.TargetSite + '\n' +
                                  "故障点：" + e.StackTrace;

        Debug.LogError(s);
#endif
    }
    public static GameObject Instantiate(GameObject go, Transform parent = null)
    {
        GameObject basePrefab;
#if UNITY_EDITOR

        if (EditorUtility.IsPersistent(go))
        {
            basePrefab = PrefabUtility.InstantiatePrefab(go, parent) as GameObject;
        }
        else
        {
            basePrefab = GameObject.Instantiate(go, parent) as GameObject;
        }
#else
        basePrefab = GameObject.Instantiate(go, parent) as GameObject;

#endif
        return basePrefab;
    }
    public static Vector3Int GetNearDir_SixDirIn3D(Vector3 dir)
    {
        dir=dir.normalized;
        int num = 0;
        float ans = Mathf.NegativeInfinity;
        for(int i = 0; i < 6; i++)
        {
            var type = (SixDirIn3DType)i;
            var temp=SixDirs[type];
            var dot = Vector3.Dot(dir, temp.ToFloat());
            if ( dot> ans)
            {
                ans = dot;
                num = i;
            }
        }
        return SixDirs[(SixDirIn3DType)num];
    }
    const int xMul = 1000000;
    const int yMul = 10000;
    //负数时键值会出错，直接使用v3int做键值
    public static int CustomV3ToInt(int x,int y,int z)
    {
        return x * xMul + y * yMul + z;
    }
    public static int CustomV3ToInt(Vector3Int v3)
    {
        return CustomV3ToInt(v3.x,v3.y,v3.z);
    }
    public static Vector3Int CustomIntToV3(int index)
    {
        int x = index / xMul;
        index = index % xMul;
        int y = index / yMul;
        int z = index % yMul;
        return new Vector3Int(x, y, z);
    }
}
public enum SixDirIn3DType
{
    Forward=0,
    Backward=1,
    Up=2,
    Down=3,
    Left=4,
    Right=5,
}
[Serializable]
public class CustomDictionary<T1,T2>where T1:IEquatable<T1>
{
    public List<T1> keys = new List<T1>();
    public List<T2> vals = new List<T2>();
    public T2 this[T1 key]
    {
        get
        {   
            var index=GetKeyIndex(key);
            if (index >= 0)
            {
                return vals[index];
            }
            Debug.LogError($"不包含该key{key}");
            return default(T2);
        }
        set
        {
            var index = GetKeyIndex(key);
            if (index == -1)
            {
                keys.Add(key);
                vals.Add(value);
            }
            else
            {
                vals[index] = value;
            }
        }
    }
    public int GetKeyIndex(T1 key)
    {
        int num = -1;
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].Equals(key))
            {
                num = i;
                break;
            }
        }
        return num;
    }   
    public bool ContainsKey(T1 key)
    {
        return GetKeyIndex(key) >= 0;
    }

    public void RemoveAt(int index)
    {
        keys.RemoveAt(index);
        vals.RemoveAt(index);
    }
    public void Remove(T1 key)
    {
        var index = GetKeyIndex(key);
        RemoveAt(index);
    }
}