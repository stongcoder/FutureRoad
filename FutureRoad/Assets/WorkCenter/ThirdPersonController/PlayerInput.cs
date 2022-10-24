using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;

	[Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;
	public void UpdateInput()
	{
		var x = Input.GetAxis("Horizontal");
		var z = Input.GetAxis("Vertical");
		move = new Vector2(x, z);
		look = Vector2.zero;
		if (Input.GetMouseButton(1))
		{
			var mouseX = Input.GetAxis("Mouse X");
			var mouseY = -1 * Input.GetAxis("Mouse Y");
			look = new Vector2(mouseX, mouseY);
		}
		jump = Input.GetKeyDown(KeyCode.Space);
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			sprint = !sprint;
		}

	}
}

