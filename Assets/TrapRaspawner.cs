using UnityEngine;
using System.Collections;

public class TrapRespawner : MonoBehaviour
{
    public GameObject trapPrefab;   // Prefab platform jatuh
    public float respawnDelay = 3f; // Waktu sebelum respawn

    private GameObject currentTrap;

    void Start()
    {
        SpawnTrap();
    }

    void SpawnTrap()
    {
        currentTrap = Instantiate(trapPrefab, transform.position, Quaternion.identity);

        // Beri tahu trap siapa respawn manager-nya
        FallingPlatform platform = currentTrap.GetComponent<FallingPlatform>();
        platform.respawner = this;
    }

    public void NotifyDestroyed()
    {
        StartCoroutine(RespawnRoutine());
    }

    IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnTrap();
    }
}
