using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // Player to follow
    public Vector3 offset = new Vector3(0, 8, -6); // Camera position offset
    public float followSpeed = 5f;   // How fast camera follows
    public float rotateSpeed = 5f;   // Rotation speed
    public float zoomSpeed = 2f;     // Scroll wheel zoom speed
    public float minZoom = 3f;       // Minimum zoom distance
    public float maxZoom = 15f;      // Maximum zoom distance

    private float currentZoom = 1f;  // Zoom factor

    // Update is called once per frame
    void LateUpdate()
    {
        if (!target) return;

        // Adjust zoom with scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom / offset.magnitude, maxZoom / offset.magnitude);

        // Move camera smoothly
        Vector3 desiredPosition = target.position + offset * currentZoom;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Rotate camera around player with right mouse drag
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            offset = Quaternion.AngleAxis(horizontal, Vector3.up) * offset;
        }

        // Always look at player
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
