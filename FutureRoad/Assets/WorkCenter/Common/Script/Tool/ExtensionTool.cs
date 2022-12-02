using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static void LocalReset(this Transform t,bool ignoreScale=false)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        if (!ignoreScale)
        {
            t.localScale = Vector3.one;
        }
    }
    public static void ClearChildren(this Transform t)
    {
        var list=new List<GameObject>();
        for(int i = 0; i < t.childCount; i++)
        {
            var child = t.GetChild(i).gameObject;
            list.Add(child);
        }
        for (int i = 0; i < list.Count; i++)
        {
            GameObject child = list[i];
            GameObject.DestroyImmediate(child);
        }
    }
    public static Vector2 ToFloat(this Vector2Int v2)
    {
        return new Vector2(v2.x, v2.y);
    }
    public static Vector3 ToFloat(this Vector3Int v3)
    {
        return new Vector3(v3.x, v3.y, v3.z);
    }
    public static Vector3Int ToInt(this Vector3 v3)
    {
        return new Vector3Int(Mathf.RoundToInt(v3.x),Mathf.RoundToInt(v3.y),Mathf.RoundToInt(v3.z));
    }
    #region System.Objcet
    public static T GetFieldValue<T>(this System.Object o, string fieldName)
    {
        try
        {
            var type = o.GetType();
            var field = type.GetField(fieldName);
            return (T)(field.GetValue(o));
        }
        catch
        {
            Debug.LogError("未找到对应字段");
            return default(T);
        }
    }
    public static void SetFiledValue<T>(this System.Object o, T value, string fieldName)
    {
        try
        {
            var type = o.GetType();
            var field = type.GetField(fieldName);
            field.SetValue(o, value);
        }
        catch
        {
            Debug.LogError("未找到对应字段");
        }

    }
    public static T GetDeepFieldValue<T>(this System.Object o, params string[] fieldNames)
    {
        try
        {
            System.Object obj = o;
            for (int i = 0; i < fieldNames.Length; i++)
            {
                obj = obj.GetFieldValue<System.Object>(fieldNames[i]);
            }
            return (T)obj;
        }
        catch
        {
            Debug.LogError("未找到对应字段");
            return default(T);
        }

    }
    public static void SetDeepFieldValue<T>(this System.Object o, T value, params string[] fieldNames)
    {
        if (fieldNames.Length <= 0) return;
        try
        {
            System.Object _SetFieldValue(System.Object o, System.Object value, List<string> fieldNames)
            {
                if (fieldNames.Count == 1)
                {
                    o.SetFiledValue<System.Object>(value, fieldNames[0]);
                    return o;
                }
                var obj = o.GetFieldValue<System.Object>(fieldNames[0]);
                var temp = fieldNames[0];
                fieldNames.RemoveAt(0);
                obj = _SetFieldValue(obj, value, fieldNames);
                o.SetFiledValue<System.Object>(obj, temp);
                return o;
            }
            _SetFieldValue(o, value, fieldNames._ToList<string>());
        }
        catch
        {
            Debug.LogError("未找到对应字段");
        }

    }
    public static List<T> _ToList<T>(this IEnumerable<T> collections)
    {
        List<T> data = new List<T>();
        var enumerator = collections.GetEnumerator();
        enumerator.Reset();
        while (enumerator.MoveNext())
        {
            var cur = enumerator.Current;
            data.Add(cur);
        }
        return data;
    }
    #endregion
}
