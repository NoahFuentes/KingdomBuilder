using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public static CameraZoom Instance { get; private set; }

    [SerializeField] private float zoomMin;
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomSensitivity;


    [SerializeField] private Cinemachine3rdPersonFollow c3pf;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        c3pf = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") == 0) return;
        float scrollVal = Input.GetAxisRaw("Mouse ScrollWheel") * -zoomSensitivity;
        float newDistance = c3pf.CameraDistance + scrollVal;

        c3pf.CameraDistance = Mathf.Clamp(newDistance, zoomMin, zoomMax);
    }

    public void GoToBuildingView()
    {
    }

    public void GoToPlayerView()
    {

    }
}
