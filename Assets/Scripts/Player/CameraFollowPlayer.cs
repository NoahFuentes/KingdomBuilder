using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform m_FollowPlayer;

    [SerializeField] private Vector3 m_CameraOffset;
    [SerializeField] private float damp = 0.45f;
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
}
