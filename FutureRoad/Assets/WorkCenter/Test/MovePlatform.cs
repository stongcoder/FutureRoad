using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTween;
public class MovePlatform : MonoBehaviour
{
    public List<Transform> wayPts = new List<Transform>();
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
        target.position = wayPts[wayPts.Count - 1].position;
        for (int i = 0; i < wayPts.Count; i++)
        {
            var pt = wayPts[i];
            float length;
            if (i == 0)
            {
                length = Vector3.Magnitude(wayPts[0].position - wayPts[wayPts.Count - 1].position);
            }
            else
            {
                length = Vector3.Magnitude(wayPts[i].position - wayPts[i - 1].position);
            }
            var moveTime = length / moveSpeed;
            seq.Append(target.TweenMove(pt.position, moveTime));

        }
        seq.SetLoop(-1);
        seq.Start();
    }
    private void ClearTween()
    {
        if (seq != null)
        {
            seq.Kill();
            seq = null;
        }
    }
}
