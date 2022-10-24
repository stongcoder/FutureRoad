using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using CustomTween;
public class Test : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 origin;
    private void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.position = origin;
        }
        var endPos = rb.position + rb.transform.forward;
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.position = endPos;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            rb.MovePosition(endPos);
        }
    }
}