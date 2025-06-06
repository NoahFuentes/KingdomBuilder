using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform m_FollowPlayer;

    //[SerializeField] private Vector3 m_CameraOffset;
    [SerializeField] private float camDistance;
    [SerializeField] private float camHeight;
    [SerializeField] private float damp;
    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomMin;
    private Vector3 velocity = Vector3.zero;

    private bool inRotationMode;
    private float yaw;
    [SerializeField] private float camSensitivity;

    private void Start()
    {
        m_FollowPlayer = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        //Vector3 desPos = m_FollowPlayer.position + m_CameraOffset;
        //transform.position = Vector3.SmoothDamp(transform.position, desPos, ref velocity, damp);
    }
    private void Update()
    {
        inRotationMode = Input.GetKey(KeyCode.Space);

        if (Input.GetAxisRaw("Mouse ScrollWheel") == 0) return;
        float scrollVal = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;
        
        //float newY = m_CameraOffset.y - scrollVal;
        //float newZ = m_CameraOffset.z + scrollVal;

        float newDist = camDistance - scrollVal;
        float newHeight = camHeight - scrollVal;

        camDistance = Mathf.Clamp(camDistance + scrollVal, zoomMin, zoomMax);
        camHeight = Mathf.Clamp(camHeight + scrollVal, zoomMin, zoomMax);

        //m_CameraOffset.y = Mathf.Clamp(newY, zoomMin, zoomMax);
        //m_CameraOffset.z = Mathf.Clamp(newZ, -zoomMax, -zoomMin);
    }

    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);
        Vector3 offset = rotation * new Vector3(0, 0, -camDistance);
        transform.position = m_FollowPlayer.position + offset + Vector3.up * camHeight;
        transform.LookAt(m_FollowPlayer.position);

        if (!inRotationMode) return;
        float mouseX = Input.GetAxis("Mouse X") * camSensitivity;
        yaw += mouseX;

        // Calculate new position

    }
}
