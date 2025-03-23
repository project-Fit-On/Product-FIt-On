using UnityEngine;

public class RotateOnDrag : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 200f;  // Adjust speed as needed

    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        // Check if left mouse button or single touch is held down
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            // Calculate how far the mouse/touch has moved horizontally
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // Rotate around Y-axis only
            // (A positive delta.x means user dragged to the right, so rotate accordingly)
            float rotationAmount = delta.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, -rotationAmount, 0f, Space.World);

            // Save current mouse position for next frame
            lastMousePosition = Input.mousePosition;
        }
    }
}
