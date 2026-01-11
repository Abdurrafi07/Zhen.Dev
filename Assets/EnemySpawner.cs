using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPointInfo
    {
        public Transform point;
        public bool vertical = false; // Pilihan arah gerak untuk spawn point
        public float moveSpeed = 2f;  // Speed musuh
        public float distance = 2f;   // Jarak musuh bergerak
        public GameObject[] enemyPrefabs;
        [HideInInspector] public bool hasSpawned = false;
    }

    public SpawnPointInfo[] spawnPoints;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        foreach (var sp in spawnPoints)
        {
            if (sp.hasSpawned) continue;
            if (sp.enemyPrefabs.Length == 0) continue;

            GameObject prefabToSpawn = sp.enemyPrefabs[Random.Range(0, sp.enemyPrefabs.Length)];
            GameObject enemy = Instantiate(prefabToSpawn, sp.point.position, Quaternion.identity);

            // Atur arah dan parameter gerak dari spawn point
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.vertical = sp.vertical;
            enemyScript.moveSpeed = sp.moveSpeed;
            enemyScript.distance = sp.distance;

            enemyScript.OnDestroyed += () =>
            {
                sp.hasSpawned = true;
            };
        }
    }
}
