using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using CustomTween;
public class Test : MonoBehaviour
{
    [EditorHandle()]
    public List<Vector3> wayPts = new List<Vector3> ();
    public Transform target;
    public float moveSpeed;
    Sequence seq;
    private void Start()
    {
        PlayTween();
    }
    [EditorTest]
    public void PlayTween()
    {
        ClearTween();
        seq = new Sequence();
        target.position=wayPts[wayPts.Count-1];
        for (int i = 0; i < wayPts.Count; i++)
        {
            var pt = wayPts[i];
            float length;
            if (i == 0)
            {
                length = Vector3.Magnitude(wayPts[0] - wayPts[wayPts.Count - 1]);
            }
            else
            {
                length = Vector3.Magnitude(wayPts[i] - wayPts[i - 1]);
            }
            var moveTime = length / moveSpeed;
            seq.Join(target.TweenLookAt(pt, 0.1f));
            seq.Append(target.TweenMove(pt, moveTime));

        }
        seq.SetLoop(-1);
        seq.Start();
    }
    private void ClearTween()
    {
        if(seq != null)
        {
            seq.Kill();
            seq = null;
        }
    }
}