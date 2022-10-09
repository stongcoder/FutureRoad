using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject ball;
    public Transform ballBirth;
    public float ballForce;
    private void Start()
    {
        StartCoroutine(ItorFire());
    }
    IEnumerator ItorFire()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Fire();
        }
    }
    private void Fire()
    {
        var go=GameObject.Instantiate(ball,ballBirth);
        go.transform.localPosition = Vector3.zero;
        var rigid=go.GetComponent<Rigidbody>();
        rigid.AddForce(ballForce*ballBirth.forward,ForceMode.Impulse);
    }
}
