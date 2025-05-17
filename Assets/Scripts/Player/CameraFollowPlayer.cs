using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform m_FollowPlayer;

    [SerializeField] private Vector3 m_CameraOffset;
    [SerializeField] private float damp = 0.45f;
    [SerializeField] private float zoomSensitivity;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        m_FollowPlayer = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector3 desPos = m_FollowPlayer.position + m_CameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, desPos, ref velocity, damp);
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") == 0) return;
        float scrollVal = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;
        
        m_CameraOffset.y -= scrollVal;
        m_CameraOffset.z += scrollVal;

        if (m_CameraOffset.y < 20f) m_CameraOffset.y = 20f;
        if (m_CameraOffset.y > 50f) m_CameraOffset.y = 50f;
        if (m_CameraOffset.z > -20f) m_CameraOffset.z = -20f;
        if (m_CameraOffset.z < -50f) m_CameraOffset.z = -50f;
    }
}
