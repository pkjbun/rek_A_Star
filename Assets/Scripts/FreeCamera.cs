using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 5f;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotateSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * rotateSpeed;

            transform.eulerAngles += new Vector3(mouseY, mouseX, 0);
        }
    }
}

