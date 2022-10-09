using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public GameObject efx;
    private void Awake()
    {
        Destroy(gameObject, 3f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var go = GameObject.Instantiate(efx);
        go.transform.position = transform.position;
        Destroy(gameObject);

    }
}
