using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public int buildingsPerRow = 12;
    public float spacing = 4f;
    public float spawnZ = 50f;
    public float yPosition = 0.5f;
    public float rowCenterX = 0f; // Use this to center your row
    private float lastSpawnZ = 0f; // Track the Z position of the last spawned row

    void Start()
    {
        lastSpawnZ = spawnZ; // Initialize the starting spawnZ
        SpawnInitialRows(); // Spawn initial rows
    }

    void Update()
    {
        // Continuously spawn new rows at the back of the current row
        // Check if the last row is far enough behind the camera to spawn a new one
        if (lastSpawnZ - Camera.main.transform.position.z < 50f) // Adjust threshold as needed
        {
            SpawnBuildingRow();
        }
    }

    void SpawnBuildingRow()
    {
        float totalWidth = spacing * (buildingsPerRow - 1);
        float startZ = lastSpawnZ; // Place the new row at the back of the previous one

        for (int i = 0; i < buildingsPerRow; i++)
        {
            int randomIndex = Random.Range(0, buildingPrefabs.Length);
            GameObject prefab = buildingPrefabs[randomIndex];

            // Calculate the Z position for each building in the row
            float zPos = startZ + i * spacing;
            Vector3 spawnPos = new Vector3(rowCenterX, yPosition, zPos);

            // Instantiate the building at the calculated position
            Instantiate(prefab, spawnPos, Quaternion.identity);
            Debug.Log($"Spawned building at Z={zPos}");
        }

        // Update the lastSpawnZ for the next row to spawn behind the current row
        lastSpawnZ = startZ + totalWidth;
    }

    // Spawn initial rows to make sure the game starts with buildings visible
    void SpawnInitialRows()
    {
        // This ensures we have some rows already spawned at the start
        for (int i = 0; i < 3; i++) // Spawn 3 initial rows
        {
            SpawnBuildingRow();
        }
    }
}
