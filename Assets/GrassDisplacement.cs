using UnityEngine;

public class GrassDisplacement : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        Shader.SetGlobalVector("_PlayerPos", player.position);
    }
}
