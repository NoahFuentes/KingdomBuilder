using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TerrainGrassSound : MonoBehaviour
{
    public Terrain terrain;
    public AudioSource grassAudioSource;
    public float checkRadius = 0.5f;
    public float maxVolume = 0.4f;
    public float fadeSpeed = 5f;

    private CharacterController controller;
    private TerrainData tData;
    private Vector3 terrainPos;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (terrain == null)
            terrain = Terrain.activeTerrain;

        tData = terrain.terrainData;
        terrainPos = terrain.transform.position;

        if (grassAudioSource != null)
            grassAudioSource.volume = 0f;
    }

    void Update()
    {
        if (grassAudioSource == null || terrain == null)
            return;

        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        bool isMoving = speed > 0.1f;

        float grassDensity = GetGrassDensityUnderPlayer();

        // Target volume depends on both speed and grass density
        float targetVolume = (isMoving && grassDensity > 0.1f)
            ? Mathf.Clamp01(speed / 6f) * grassDensity * maxVolume
            : 0f;

        grassAudioSource.volume = Mathf.Lerp(grassAudioSource.volume, targetVolume, Time.deltaTime * fadeSpeed);

        if (grassAudioSource.volume > 0.01f && !grassAudioSource.isPlaying)
            grassAudioSource.Play();
        else if (grassAudioSource.volume <= 0.01f && grassAudioSource.isPlaying)
            grassAudioSource.Stop();
    }

    float GetGrassDensityUnderPlayer()
    {
        Vector3 playerPos = transform.position - terrainPos;
        Vector3 normalizedPos = new Vector3(
            Mathf.InverseLerp(0, tData.size.x, playerPos.x),
            0,
            Mathf.InverseLerp(0, tData.size.z, playerPos.z)
        );

        int x = Mathf.RoundToInt(normalizedPos.x * tData.detailWidth);
        int z = Mathf.RoundToInt(normalizedPos.z * tData.detailHeight);

        float densitySum = 0;
        int layerCount = tData.detailPrototypes.Length;

        for (int i = 0; i < layerCount; i++)
        {
            int[,] detailLayer = tData.GetDetailLayer(x, z, 1, 1, i);
            densitySum += detailLayer[0, 0];
        }

        // Normalize (0–1 range)
        return Mathf.Clamp01(densitySum / 16f);
    }
}
