/* This script is applied to the main camera of the scene and makes it follow the players transform + the specified
 * offset. The scroll wheel allows for zoom in and out.
 */


using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform m_FollowPlayer;

    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float damp;
    [SerializeField] private float zoomSensitivity;
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomMin;
    private Vector3 velocity = Vector3.zero;

    private bool inRotationMode;
    [SerializeField] private float camSensitivity;

    private void Start()
    {
        m_FollowPlayer = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector3 desPos = m_FollowPlayer.position + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, desPos, ref velocity, damp);
    }
    
    private void Update()
    {

        if (Input.GetAxisRaw("Mouse ScrollWheel") == 0) return;
        float scrollVal = Input.GetAxisRaw("Mouse ScrollWheel") * zoomSensitivity;
        
        float newY = cameraOffset.y - scrollVal;
        float newZ = cameraOffset.z + scrollVal;

        cameraOffset.y = Mathf.Clamp(newY, zoomMin, zoomMax);
        cameraOffset.z = Mathf.Clamp(newZ, -zoomMax, -zoomMin);
    }
    
}
