using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulRemake
{
    public class PlayerInput : MonoBehaviour
    {
        public string keyUp="w";
        public string keyDown = "s";
        public string keyLeft = "a";
        public string keyRight = "d";
        public string keyRun = "left shift";
        public string keyJump = "space";
        public string keyLockOn = "l";
        public bool planarInputEnabled=true;
        public float smoothTime = 0.5f;

        [DisplayOnly] public float dUpTarget;
        [DisplayOnly] public float dRightTarget;
        [DisplayOnly] public float dUp;
        [DisplayOnly] public float dRight;
        [DisplayOnly] public float dMag;
        [DisplayOnly] public Vector3 dVec;
        [DisplayOnly] public float cRight;
        [DisplayOnly] public float cUp;
        [DisplayOnly] public bool canRun;
        [DisplayOnly] public bool canJump;
        [DisplayOnly] public bool canAttack;
        [DisplayOnly] public bool canDefense;
        [DisplayOnly] public bool lockOn;
        float upVelocity;
        float rightVelocity;
        private void Update()
        {
            dUpTarget = (Input.GetKey(keyUp) ? 1.0f : 0f) - (Input.GetKey(keyDown) ? 1.0f : 0f);
            dRightTarget = (Input.GetKey(keyRight) ? 1.0f : 0f)- (Input.GetKey(keyLeft)? 1.0f : 0f);
            if (!planarInputEnabled)
            {
                dUpTarget = 0f;
                dRightTarget= 0f;
            }
            dUp = Mathf.SmoothDamp(dUp, dUpTarget, ref upVelocity, smoothTime);
            dRight=Mathf.SmoothDamp(dRight,dRightTarget,ref rightVelocity, smoothTime);
            var mapping=SquareToCircle(new Vector2(dRight,dUp));
            dMag = mapping.magnitude;
            dVec = (mapping.y * transform.forward + mapping.x * transform.right);
            canRun = Input.GetKey(keyRun);
            canDefense = Input.GetMouseButton(1);
            canAttack=Input.GetMouseButtonDown(0);
            canJump=Input.GetKeyDown(keyJump);
            lockOn = Input.GetKeyDown(keyLockOn);
            cRight = Input.GetAxis("Mouse X");
            cUp = Input.GetAxis("Mouse Y");
        }
        //Õ÷‘≤”≥…‰∑®
        private Vector2 SquareToCircle(Vector2 input)
        {
            Vector2 output = Vector2.zero;
            output.x=input.x*Mathf.Sqrt(1-(input.y*input.y)/2f);
            output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2f);
            return output;
        }
    }
}

