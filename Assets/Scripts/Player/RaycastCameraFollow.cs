using UnityEngine;

public class RaycastCameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform character;         // The target to follow
    public Transform cameraTransform;   // The camera to move

    [Header("Orbit Settings")]
    public float mouseSensitivity = 2f; // Mouse look sensitivity
    public float minPitch = -30f;       // How far down you can look
    public float maxPitch = 60f;        // How far up you can look
    public Vector3 pivotOffset = new Vector3(0, 1.5f, 0); // Offset for orbit pivot

    [Header("Zoom Settings")]
    public float minZoom = 2f;          // Closest zoom distance
    public float maxZoom = 8f;          // Farthest zoom distance
    public float zoomSpeed = 2f;        // How fast zoom reacts to scroll
    private float targetZoom;           // Desired zoom distance

    [Header("Camera Settings")]
    public LayerMask collisionMask;     // Layers that block camera
    public float smoothSpeed = 10f;     // Smoothing for camera movement

    private float yaw;
    private float pitch;

    private void Start()
    {
        // Initialize yaw/pitch from current rotation
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        targetZoom = Mathf.Clamp((minZoom + maxZoom) * 0.5f, minZoom, maxZoom); // Start at mid zoom

        Cursor.lockState = CursorLockMode.Locked; // Lock cursor (optional)
        Cursor.visible = false;                   // Hide cursor (optional)

        character = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (!character || !cameraTransform) return;

        // --- Mouse Input (orbit) ---
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // --- Scroll Wheel Zoom ---
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            targetZoom -= scroll * zoomSpeed;
            targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom);
        }

        // Direction from yaw/pitch
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 rayDir = rotation * Vector3.back;

        // Ray start (pivot point above character)
        Vector3 rayStart = character.position + pivotOffset;

        // Default position (max distance behind target)
        Vector3 targetPos = rayStart + rayDir * targetZoom;

        // Raycast to prevent clipping into walls
        if (Physics.Raycast(rayStart, rayDir, out RaycastHit hit, targetZoom, collisionMask))
        {
            targetPos = hit.point;
        }

        // Smooth camera movement
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPos, smoothSpeed * Time.deltaTime);

        // Always look at the pivot
        cameraTransform.LookAt(rayStart);
    }
}
