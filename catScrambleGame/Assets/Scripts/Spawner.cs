using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;       // All your obstacles
    public GameObject[] powerupPrefabs;        // All your powerups (like hearts)
    public float powerupSpawnChance = 0.15f;   // Chance to spawn powerup

    public Vector3 boxSize = new Vector3(10, 1, 20);
    public Vector3 spawnCenter = Vector3.zero;
    public Vector3 moveDirection = Vector3.back;
    public float spawnInterval = 1.0f;
    public float moveSpeed = 2.0f;
    public float despawnZ = -20.0f;

    public float[] spawnXOptions = new float[4];

    public float spawnRateIncreaseInterval = 40f; // Every 40 seconds
    public float spawnRateMultiplier = 0.9f;      // Decrease interval by 10%
    public float minSpawnInterval = 0.2f;         // Cap at 0.2s interval

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, spawnInterval);
        InvokeRepeating(nameof(IncreaseSpawnRate), spawnRateIncreaseInterval, spawnRateIncreaseInterval);
    }

    void Spawn()
    {
        float x = spawnXOptions[Random.Range(0, spawnXOptions.Length)];
        float y = Random.Range(-boxSize.y / 2, boxSize.y / 2);
        float z = Random.Range(boxSize.z / 2, boxSize.z);

        Vector3 spawnPos = spawnCenter + new Vector3(x, y, z);

        Debug.Log("Spawn Position: " + spawnPos);

        GameObject prefabToSpawn;
        bool isPowerup = false;

        if (Random.value < powerupSpawnChance && powerupPrefabs.Length > 0)
        {
            prefabToSpawn = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];
            isPowerup = true;
        }
        else
        {
            prefabToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        }

        GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        // Assign tag
        obj.tag = isPowerup ? "Powerup" : "Obstacle";

        // Scale
        obj.transform.localScale = Vector3.one * (isPowerup ? 30f : 10f);

        // Add Mover component
        obj.AddComponent<Mover>().Initialize(moveDirection, moveSpeed, despawnZ);

        // Add BoxCollider with trigger enabled
        BoxCollider collider = obj.AddComponent<BoxCollider>();
        collider.isTrigger = true;
    }

    void IncreaseSpawnRate()
    {
        float newInterval = Mathf.Max(minSpawnInterval, spawnInterval * spawnRateMultiplier);

        if (newInterval < spawnInterval)
        {
            spawnInterval = newInterval;

            // Restart spawn with new interval
            CancelInvoke(nameof(Spawn));
            InvokeRepeating(nameof(Spawn), 0f, spawnInterval);

            Debug.Log($"Spawn rate increased! New interval: {spawnInterval:F2}s");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnCenter, boxSize);
    }
}
