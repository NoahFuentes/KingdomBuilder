using UnityEngine;

public class BuildRangeRotation : MonoBehaviour
{
    [SerializeField] private float rotSpeed;
    private void FixedUpdate()
    {
        Vector3 rot = new Vector3(0f, 0f, rotSpeed);
        transform.Rotate(rot);
    }
}
