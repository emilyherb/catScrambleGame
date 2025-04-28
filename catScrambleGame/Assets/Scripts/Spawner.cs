using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public GameObject[] powerupPrefabs;
    public float powerupSpawnChance = 0.15f;

    public Vector3 boxSize = new Vector3(10, 1, 20);
    public Vector3 spawnCenter = Vector3.zero;
    public Vector3 moveDirection = Vector3.back;
    public float spawnInterval = 1.0f;
    public float moveSpeed = 2.0f;
    public float despawnZ = -20.0f;

    public float[] spawnXOptions = new float[4];

    public float spawnRateIncreaseInterval = 40f;
    public float spawnRateMultiplier = 0.9f;
    public float minSpawnInterval = 0.2f;

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

        obj.tag = isPowerup ? "Powerup" : "Obstacle";

        obj.transform.localScale = Vector3.one * (isPowerup ? 30f : 10f);

        obj.AddComponent<Mover>().Initialize(moveDirection, moveSpeed, despawnZ);

        if (obj.GetComponent<BoxCollider>() == null)
        {
            Debug.LogWarning($"Spawned object {obj.name} does not have a BoxCollider! Please add one manually.");
        }
        else
        {
            BoxCollider collider = obj.GetComponent<BoxCollider>();
            if (!collider.isTrigger)
            {
                Debug.LogWarning($"BoxCollider on {obj.name} is not set as a trigger. Setting it now.");
                collider.isTrigger = true;
            }
        }
    }

    void IncreaseSpawnRate()
    {
        float newInterval = Mathf.Max(minSpawnInterval, spawnInterval * spawnRateMultiplier);

        if (newInterval < spawnInterval)
        {
            spawnInterval = newInterval;

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
