using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class ScreenCamera : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform player;
    public CinemachineFreeLook freeLook;
    public float XSensitivity = 0.01f;
    public float YSensitivity = 1;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        var x = eventData.delta.x;
        var y = eventData.delta.y;
        var speed1 = y * YSensitivity;
        var val1 = freeLook.m_YAxis.Value + speed1;
        freeLook.m_YAxis.Value = Mathf.Clamp(val1, 0, 1f);
        var speed2 = x * XSensitivity;
        var val2 = freeLook.m_XAxis.Value + speed2;
        freeLook.m_XAxis.Value = val2;        
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
    }
}
