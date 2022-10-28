using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulRemake
{
    public class ActorController : MonoBehaviour
    {
        public Animator anim;
        public PlayerInput input;
        public Rigidbody rb;
        public GameObject model;
        public float walkSpeed = 1f;
        public float runMultiplier = 2f;
        public float jumpVelocity;
        [SerializeField] PhysicMaterial oneFriction;
        [SerializeField] PhysicMaterial zeroFriction;
        [SerializeField] Collider col;
        private Vector3 planarVec;
        private bool lockPlanar;
        private Vector3 thrustVec;
        private bool canAttack;
        Vector3 deltaPos;
        #region 动画参数
        string anim_forward = "forward";
        string anim_jump = "jump";
        string anim_isGround = "isGround";
        string anim_jabVelocity = "jabVelocity";
        string anim_attack = "attack";
        string anim_attack1hAVelocity= "attack1hAVelocity";
        string anim_defense = "defense";

        string layer_move = "move";
        string layer_attack = "attack";

        string state_ground = "ground";
        string state_attack1hC = "attack1hC";
        #endregion

        private void Update()
        {
            anim.SetFloat(anim_forward,input.dMag* walkSpeed * (input.canRun ? runMultiplier : 1f));
            anim.SetBool(anim_defense, input.canDefense);
            if (input.canJump)
            {
                anim.SetTrigger(anim_jump);
                canAttack = false;
            }
            if (input.canAttack&&CheckAnimState(state_ground)&&canAttack)
            {
                anim.SetTrigger(anim_attack);
            }
            if (input.dMag > 0.01f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, input.dVec, 0.3f);
            }
            if (!lockPlanar)
            {
                planarVec = input.dMag * model.transform.forward * walkSpeed * (input.canRun ? runMultiplier : 1f);
            }
        }
        private void FixedUpdate()
        {
            //rb.position += move * Time.fixedDeltaTime; 
            rb.position += deltaPos;
            deltaPos = Vector3.zero;
            rb.velocity = new Vector3(planarVec.x, rb.velocity.y, planarVec.z) + thrustVec ;
            thrustVec = Vector3.zero;
        }
        private bool CheckAnimState(string name, string layerName = "move")
        {
            var layerIndex = anim.GetLayerIndex(layerName);
            return anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(name);
        }
        #region OnGroundSensor消息
        public void IsOnGround()
        {
            anim.SetBool(anim_isGround, true);
        }
        public void IsNotOnGround()
        {
            anim.SetBool(anim_isGround, false);
        }
        #endregion
        #region 动画脚本消息
        public void OnJumpEnter()
        {
            Debug.Log("JumpEnter");
            thrustVec = new Vector3(0, jumpVelocity, 0);
            LPAndEPI();
        }
        public void OnJumpExit()
        {
            Debug.Log("JumpExit");
            
        }
        public void OnGroundEnter()
        {
            ULPAndDPI();
            canAttack = true;
            col.material = oneFriction;
        }
        public void OnGroundExit()
        {
            LPAndEPI();
            col.material = zeroFriction;
        }
        public void OnJabEnter()
        {
            LPAndEPI();
        }
        public void OnJabUpdate()
        {
            thrustVec = model.transform.forward *anim.GetFloat(anim_jabVelocity);
        }
        float layerLerpTarget;
        public void OnAttack1hAEnter()
        {
            DisablePlanarInput();
            layerLerpTarget = 1f;
        }
        public void OnAttack1hAUpdate()
        {
            thrustVec = model.transform.forward * anim.GetFloat(anim_attack1hAVelocity);
            var index = anim.GetLayerIndex(layer_attack);
            float curWeight = anim.GetLayerWeight(index);
            curWeight = Mathf.Lerp(curWeight, layerLerpTarget, 0.5f);
            anim.SetLayerWeight(index,curWeight);

        }
        public void OnAttackIdleEnter()
        {
            var index = anim.GetLayerIndex(layer_attack);
            anim.SetLayerWeight(index, 0f);
            EnablePlanarInput();
            layerLerpTarget = 0f;
        }
        public void OnAttackIdleUpdate()
        {
            var index = anim.GetLayerIndex(layer_attack);
            float curWeight = anim.GetLayerWeight(index);
            curWeight = Mathf.Lerp(curWeight, layerLerpTarget, 0.5f);
            anim.SetLayerWeight(index, curWeight);
        }
        public void OnUpdateRootMotion(System.Object obj)
        {
            if (CheckAnimState(state_attack1hC, layer_attack))
            {
                deltaPos += (Vector3)obj;
            }
        }
        #endregion
        private void LPAndEPI()
        {
            lockPlanar = true;
            input.planarInputEnabled = false;
        }
        private void ULPAndDPI()
        {
            lockPlanar = false;
            input.planarInputEnabled = true;
        }
        private void DisablePlanarInput()
        {
            input.planarInputEnabled = false;

        }
        private void EnablePlanarInput()
        {
            input.planarInputEnabled = true;

        }
    }
}

