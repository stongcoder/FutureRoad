using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePerspective : MonoBehaviour
{
    public GameObject firstObj;
    public GameObject thirdObj;
    public FreeMoveCamera freeCam;
    public PlayerCC cc;
    public PlayerRB rb;
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
        if(cc!= null)
        {
            freeCam.MoveTo(cc.CinemachineCameraTarget.transform.position);
        }
        else
        {
            freeCam.MoveTo(rb.CinemachineCameraTarget.transform.position);

        }
    }
    public void ChangeToThird()
    {
        isFirst = false;
        firstObj.SetActive(false);
        thirdObj.SetActive(true);
        if (cc != null)
        {
            cc.MoveTo(freeCam.transform.position);
        }
        else
        {
            rb.MoveTo(freeCam.transform.position);
        }
    }
}
