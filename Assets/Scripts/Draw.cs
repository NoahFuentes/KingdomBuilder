using UnityEngine;
using System.Collections;

public class Draw : MonoBehaviour
{
    public static Draw Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void DrawBox(Vector3 boxCenter, Quaternion rotation, Vector3 boxSize, Color color)
    {
        StartCoroutine(DrawBoxCoroutine(boxCenter, rotation, boxSize, color, 0.5f));
    }

    IEnumerator DrawBoxCoroutine(Vector3 center, Quaternion rotation, Vector3 size, Color color, float duration)
    {
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            DrawWireBox(center, rotation, size, color);
            yield return null;
        }
    }

    void DrawWireBox(Vector3 center, Quaternion rotation, Vector3 size, Color color)
    {
        Vector3 halfSize = size * 0.5f;
        Vector3[] corners = new Vector3[8];

        // Local corners before rotation
        for (int i = 0; i < 8; i++)
        {
            Vector3 localCorner = new Vector3(
                ((i & 1) == 0 ? -1 : 1) * halfSize.x,
                ((i & 2) == 0 ? -1 : 1) * halfSize.y,
                ((i & 4) == 0 ? -1 : 1) * halfSize.z
            );

            // Apply rotation and translate to world position
            corners[i] = center + rotation * localCorner;
        }

        // Draw box edges
        Debug.DrawLine(corners[0], corners[1], color);
        Debug.DrawLine(corners[1], corners[3], color);
        Debug.DrawLine(corners[3], corners[2], color);
        Debug.DrawLine(corners[2], corners[0], color);

        Debug.DrawLine(corners[4], corners[5], color);
        Debug.DrawLine(corners[5], corners[7], color);
        Debug.DrawLine(corners[7], corners[6], color);
        Debug.DrawLine(corners[6], corners[4], color);

        Debug.DrawLine(corners[0], corners[4], color);
        Debug.DrawLine(corners[1], corners[5], color);
        Debug.DrawLine(corners[2], corners[6], color);
        Debug.DrawLine(corners[3], corners[7], color);
    }

}
