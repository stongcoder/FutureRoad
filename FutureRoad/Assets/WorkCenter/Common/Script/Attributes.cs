using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class EditorTestAttribute : Attribute
{
    public List<System.Object> objs;
    public EditorTestAttribute(params System.Object[] objs)
    {
        this.objs = new List<object>();
        foreach (var obj in objs)
        {
            this.objs.Add(obj);
        }
    }
    
}
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
public class EditorHandleAttribute : Attribute
{
    public List<System.Object> objs;
    public EditorHandleAttribute()
    {        
    }
}