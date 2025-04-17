using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;    // All your obstacles
    public GameObject[] powerupPrefabs;     // All your powerups (like hearts)
    public float powerupSpawnChance = 0.15f; // chance to spawn powerup

    public Vector3 boxSize = new Vector3(10, 1, 20);
    public Vector3 spawnCenter = Vector3.zero;
    public Vector3 moveDirection = Vector3.back;
    public float spawnInterval = 1.0f;
    public float moveSpeed = 2.0f;
    public float despawnZ = -20.0f;

    public float[] spawnXOptions = new float[4];

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0f, spawnInterval);
    }

void Spawn()
{
    float x = spawnXOptions[Random.Range(0, spawnXOptions.Length)];
    float y = Random.Range(-boxSize.y / 2, boxSize.y / 2);
    float z = Random.Range(boxSize.z / 2, boxSize.z);

    Vector3 spawnPos = spawnCenter + new Vector3(x, y, z);

    Debug.Log("Spawn Position: " + spawnPos);

    GameObject prefabToSpawn;

    if (Random.value < powerupSpawnChance && powerupPrefabs.Length > 0)
    {
        prefabToSpawn = powerupPrefabs[Random.Range(0, powerupPrefabs.Length)];
    }
    else
    {
        prefabToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
    }

    GameObject obj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
    if (prefabToSpawn.CompareTag("Powerup"))
{
    obj.transform.localScale = Vector3.one * 30f;
}
else
{
    obj.transform.localScale = Vector3.one * 10f;
}

    obj.AddComponent<Mover>().Initialize(moveDirection, moveSpeed, despawnZ);
}


 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnCenter, boxSize);
    }
}
