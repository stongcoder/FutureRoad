using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulRemake
{
    public class CameraController : MonoBehaviour
    {
        public ActorController player;
        public PlayerInput input;
        public Camera cam;
        public float mouseSensitivity = 3;
        public float camLimit = 60f;
        [DisplayOnly] public GameObject lockOnTarget;
        public float lockOnLength = 5f;
        public float lockOnWidth = 0.5f;
        public GameObject lockImg;
        public bool lockState;
        private void Update()
        {
            if (lockOnTarget == null)
            {
                var mouseX = input.cRight * mouseSensitivity;
                var mouseY = -1 * input.cUp * mouseSensitivity;
                var angY = mouseX;
                player.transform.Rotate(player.transform.up, angY);
                var angX = transform.eulerAngles.x + mouseY;
                angX = HelperTool.Angle360To180(angX);
                if (angX <= camLimit && angX >= -camLimit)
                {
                    transform.Rotate(new Vector3(mouseY, 0f, 0f), Space.Self);
                }     
                lockImg.SetActive(false);
                lockState = false;
            }
            else
            {
                var forward= lockOnTarget.transform.position - player.transform.position;
                forward.y = 0f;
                player.transform.forward = forward;               
                lockImg.SetActive(true);
                UpdateLockDotPos();
                lockState = true;
            }
            if (input.lockOn)
            {
                TryLockOn();
            }
        }
        void UpdateLockDotPos()
        {
            var worldPos = lockOnTarget.transform.position;
            Vector3 screenPoint = cam.WorldToScreenPoint(worldPos);
            Vector2 uiPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(lockImg.transform.parent.GetComponent<RectTransform>(),
                screenPoint,null, out uiPosition);
            lockImg.GetComponent<RectTransform>().anchoredPosition = uiPosition;
        }
        private void TryLockOn()
        {
            var start = player.model.transform.position + Vector3.up;
            var center = start + player.model.transform.forward * lockOnLength / 2f;
            var cols = Physics.OverlapBox(center, new Vector3(lockOnWidth / 2f, lockOnWidth / 2f, lockOnLength / 2f),
                player.transform.rotation, LayerMask.GetMask("Enemy"));
            if (cols.Length>0)
            {
                foreach(var col in cols)
                {
                    if (col.gameObject == lockOnTarget) continue;
                    lockOnTarget = col.gameObject;
                    break;
                }
            }
            else
            {
                lockOnTarget = null;
            }
        }
    }
}

