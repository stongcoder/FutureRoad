using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePerspective : MonoBehaviour
{
    public GameObject firstObj;
    public GameObject thirdObj;
    public FreeMoveCamera freeCam;
    public ThirdPersonController tpc;
    bool isFirst;
    private void Start()
    {
        isFirst = false;
        thirdObj.SetActive(true);
        firstObj.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isFirst)
            {
                ChangeToThird();
            }
            else
            {
                ChangeToFirst();
            }
        }
    }
    public void ChangeToFirst()
    {
        isFirst = true;
        thirdObj.SetActive(false);
        firstObj.SetActive(true);
        freeCam.MoveTo(tpc.CinemachineCameraTarget.transform.position);
    }
    public void ChangeToThird()
    {
        isFirst = false;
        firstObj.SetActive(false);
        thirdObj.SetActive(true);
        tpc.MoveTo(freeCam.transform.position);
    }
}
