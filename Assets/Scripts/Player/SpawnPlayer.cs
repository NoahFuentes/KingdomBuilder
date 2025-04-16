using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    private void Awake()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
