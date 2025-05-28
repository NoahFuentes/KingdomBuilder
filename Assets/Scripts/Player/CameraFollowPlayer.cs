using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform m_FollowPlayer;

    [SerializeField] private Vector3 m_CameraOffset;
    [SerializeField] private float damp = 0.45f;
    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomMin;
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
        
        float newY = m_CameraOffset.y - scrollVal;
        float newZ = m_CameraOffset.z + scrollVal;

        m_CameraOffset.y = Mathf.Clamp(newY, zoomMin, zoomMax);
        m_CameraOffset.z = Mathf.Clamp(newZ, -zoomMax, -zoomMin);
    }
}
