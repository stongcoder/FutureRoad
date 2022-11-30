using UnityEngine;

public class FreeMoveCamera : MonoBehaviour
{
    public float speed = 10f;
    public float mouseSensitivity = 3;
    public float camLimit = 60f;
    public float zoomSensity = 5f;
    public float dragSensity = 1;
    public int accelerate = 1;
    public void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            accelerate += 3;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            accelerate -= 3;
        }
        accelerate = accelerate > 1 ? accelerate : 1;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            accelerate = 1;
        }
        if (Input.GetMouseButton(1))
        {
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            var mouseY = -1*Input.GetAxis("Mouse Y") * mouseSensitivity;
            var angX = transform.eulerAngles.x + mouseY;
            var angY = transform.eulerAngles.y + mouseX;
            Vector3 euler=transform.eulerAngles;
            angX=HelperTool.Angle360To180(angX);
            if(angX > camLimit)
            {
                euler.x = camLimit;
            }else if (angX < -camLimit)
            {
                euler.x = -camLimit;
            }
            else
            {
                euler.x = angX;
            }
            euler.y = angY;
            transform.rotation=Quaternion.Euler(euler);
        }
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var y = Input.GetKey(KeyCode.E) ? 1 : 0;
        if (y == 0)
        {
            y=Input.GetKey(KeyCode.Q) ? -1 : 0;
        }
        var forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

        var right = Vector3.Cross(Vector3.up, forward).normalized;
        transform.position += (forward * z + right * x + y * Vector3.up) * Time.deltaTime * speed*accelerate;

        var mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += mouseScroll  * transform.forward  * zoomSensity*accelerate;

        if (Input.GetMouseButton(2))
        {
            var pos = transform.position - transform.right * Input.GetAxisRaw("Mouse X")*dragSensity*accelerate;
            pos = pos - transform.up * Input.GetAxisRaw("Mouse Y")*dragSensity*accelerate;
            transform.position = pos;
        }
    }
}
