using UnityEngine;

public class BodyRotation : MonoBehaviour
{
    public float rotationSpeed = 300f;        // Speed multiplier for user interaction
    public float constantSpinSpeed = 20f;       // Slow constant spin speed (degrees per second)

    void Update()
    {
        // Constant rotation around the y-axis
        transform.Rotate(Vector3.up, constantSpinSpeed * Time.deltaTime, Space.World);

        // User-controlled rotation when the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float rotY = mouseX * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, -rotY, Space.World);
        }
    }
}
