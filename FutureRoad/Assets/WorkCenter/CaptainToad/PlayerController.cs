using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CaptainToad
{
    public class PlayerController : MonoBehaviour
    {
        public Animator anim;
        public PlayerInput input;
        public Rigidbody rb;
        public GameObject model;
        public float walkSpeed = 1f;
        private Vector3 planarVec;
        private bool lockPlanar;
        private Vector3 thrustVec;
        Vector3 deltaPos;
        public Transform cam;
        public GameObject weapon;
        public Collider attackCol;
        #region ¶¯»­²ÎÊý
        string anim_forward = "forward";
        string anim_right = "right";
        #endregion
        private void Awake()
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }
        private void Update()
        {
            //CheckAnimState(state_ground)
            var dir = cam.TransformVector(input.dVec);
            dir = Vector3.ProjectOnPlane(dir, Vector3.up).normalized;
            transform.forward = Vector3.Slerp(transform.forward, dir, 0.3f);
            planarVec= input.dMag* transform.forward* walkSpeed;
            anim.SetFloat("speed",input.dMag);
        }
        public void StartAttack()
        {
            weapon.SetActive(true);
            attackCol.gameObject.SetActive(true);
            anim.SetBool("attack",true);
        }
        public void StopAttack()
        {
            weapon.SetActive(false);
            attackCol.gameObject.SetActive(false);
            anim.SetBool("attack",false);
        }
        private void FixedUpdate()
        {
            rb.velocity = new Vector3(planarVec.x, rb.velocity.y, planarVec.z) + thrustVec;
            thrustVec = Vector3.zero;
        }
        private bool CheckAnimState(string name, string layerName = "move")
        {
            var layerIndex = anim.GetLayerIndex(layerName);
            return anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(name);
        }
        private void OnTriggerEnter(Collider other)
        {
            LevelManager.Instance.ProcessTrigger(other, InteractType.Enter);
        }
        private void OnTriggerExit(Collider other)
        {
            LevelManager.Instance.ProcessTrigger(other, InteractType.Exit);
        }
    }

}
