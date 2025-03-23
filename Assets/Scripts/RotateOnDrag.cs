using UnityEngine;

public class RotateOnDrag : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 200f;  // Adjust speed as needed

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse down! Starting drag.");
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse up! Ending drag.");
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Debug.Log("Dragging... delta.x = " + delta.x);

            float rotationAmount = delta.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, -rotationAmount, 0f, Space.World);

            lastMousePosition = Input.mousePosition;
        }
    }

}
