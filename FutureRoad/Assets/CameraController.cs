using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulRemake
{
    public class CameraController : MonoBehaviour
    {
        public ActorController player;
        public PlayerInput input;
        public Transform cam;
        public float mouseSensitivity = 3;
        public float camLimit = 60f;
        private void Update()
        {
            var mouseX = input.cRight * mouseSensitivity;
            var mouseY = -1 *input.cUp* mouseSensitivity;
            var angY = mouseX;            
            player.transform.Rotate(player.transform.up, angY);

            var angX = transform.eulerAngles.x + mouseY;
            angX = HelperTool.Angle360To180(angX);
            if (angX <= camLimit && angX >= -camLimit)
            {
                transform.Rotate(new Vector3(mouseY,0f,0f),Space.Self);
            }
        }
    }
}

