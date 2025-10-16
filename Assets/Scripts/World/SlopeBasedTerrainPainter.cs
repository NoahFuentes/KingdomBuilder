using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SlopeBasedTerrainPainter : MonoBehaviour
{
    [System.Serializable]
    public class SlopeLayer
    {
        public TerrainLayer terrainLayer;
        [Range(0f, 90f)] public float minSlope = 0f;
        [Range(0f, 90f)] public float maxSlope = 90f;
        [Range(0.01f, 1f)] public float blend = 0.2f;
    }

    [Header("Terrain")]
    public Terrain terrain;

    [Header("Slope Layers")]
    public List<SlopeLayer> layers = new List<SlopeLayer>();

    [ContextMenu("Apply Slope Textures")]
    public void ApplySlopeTextures()
    {
        if (!terrain)
        {
            Debug.LogError("Assign a terrain first.");
            return;
        }

        TerrainData data = terrain.terrainData;
        int res = data.alphamapResolution;

        if (layers.Count == 0)
        {
            Debug.LogError("Add at least one slope layer!");
            return;
        }

        // --- Setup terrain layers ---
        TerrainLayer[] terrainLayers = new TerrainLayer[layers.Count];
        for (int i = 0; i < layers.Count; i++)
            terrainLayers[i] = layers[i].terrainLayer;
        data.terrainLayers = terrainLayers;

        float[,,] alphaMap = new float[res, res, layers.Count];

        // --- Compute slope-based texture distribution ---
        for (int y = 0; y < res; y++)
        {
            for (int x = 0; x < res; x++)
            {
                Vector3 normal = data.GetInterpolatedNormal((float)x / res, (float)y / res);
                float slope = Vector3.Angle(Vector3.up, normal);

                float total = 0f;
                float[] weights = new float[layers.Count];

                for (int i = 0; i < layers.Count; i++)
                {
                    SlopeLayer layer = layers[i];
                    float t = Mathf.InverseLerp(layer.minSlope, layer.maxSlope, slope);
                    float edgeBlend = layer.blend;
                    t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01((t - (0.5f - edgeBlend / 2f)) / edgeBlend));
                    float weight = Mathf.Clamp01(1f - Mathf.Abs(t - 0.5f) * 2f);

                    weights[i] = weight;
                    total += weight;
                }

                for (int i = 0; i < layers.Count; i++)
                    alphaMap[y, x, i] = total > 0 ? weights[i] / total : (i == 0 ? 1f : 0f);
            }
        }

        data.SetAlphamaps(0, 0, alphaMap);
        Debug.Log("? Slope textures applied!");
    }
}

[CustomEditor(typeof(SlopeBasedTerrainPainter))]
public class SlopeBasedTerrainPainterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SlopeBasedTerrainPainter painter = (SlopeBasedTerrainPainter)target;

        EditorGUILayout.HelpBox("Automatically paints the terrain based on slope angle.", MessageType.Info);

        DrawDefaultInspector();

        if (GUILayout.Button("Apply Slope Textures"))
        {
            painter.ApplySlopeTextures();
        }
    }
}
