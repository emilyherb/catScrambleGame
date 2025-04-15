using UnityEngine;
using System.Collections.Generic;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public int poolSizePerSide = 10;
    public float spawnZStart = 20f;
    public float spawnDistance = 10f;
    public float despawnZ = -10f;
    public float speed = 5f;

    public Transform buildingSpawnOrigin;
    public float sideOffsetX = 6f; // distance from center to left/right buildings

    private List<GameObject> leftBuildings = new List<GameObject>();
    private List<GameObject> rightBuildings = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSizePerSide; i++)
        {
            SpawnBuilding(leftBuildings, -sideOffsetX, i);
            SpawnBuilding(rightBuildings, sideOffsetX, i);
        }
    }

    void Update()
    {
        MoveBuildings(leftBuildings);
        MoveBuildings(rightBuildings);
    }

    void SpawnBuilding(List<GameObject> pool, float xOffset, int index)
    {
        GameObject prefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Length)];
        Vector3 pos = buildingSpawnOrigin.position + new Vector3(xOffset, 0f, index * spawnDistance);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity, transform);
        pool.Add(obj);
    }

    void MoveBuildings(List<GameObject> pool)
    {
        foreach (GameObject building in pool)
        {
            building.transform.Translate(Vector3.back * speed * Time.deltaTime);

            if (building.transform.position.z < despawnZ)
            {
                float maxZ = GetFurthestZ(pool);
                Vector3 newPos = new Vector3(
                    building.transform.position.x,
                    building.transform.position.y,
                    maxZ + spawnDistance
                );
                building.transform.position = newPos;
            }
        }
    }

    float GetFurthestZ(List<GameObject> pool)
    {
        float maxZ = float.MinValue;
        foreach (GameObject obj in pool)
        {
            if (obj.transform.position.z > maxZ)
                maxZ = obj.transform.position.z;
        }
        return maxZ;
    }
}
