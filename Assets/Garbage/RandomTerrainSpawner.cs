using System.Collections;
using UnityEngine;

public class RandomTerrainSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private Transform playerTransform;

    public float minSpawnRadius = 5f;

    public float maxSpawnRadius = 10f;

    public int numberOfObjects = 5;

    public Terrain spawnTerrain;

    public float minDistanceBetweenObjects = 2f;

    public int maxSpawnAttempts = 50;

    public int spawnDuration = 20;

    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        SpawnObjectsAroundPlayer();
        StartCoroutine(SpawnDurationHandler());
    }
    IEnumerator SpawnDurationHandler()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(spawnDuration);
            SpawnObjectsAroundPlayer();
        }
    }

    void SpawnObjectsAroundPlayer()
    {
        print("Object spawned");
        if (playerTransform == null || objectToSpawn == null)
        {
            Debug.LogError("Player Transform or Object to Spawn is not assigned!");
            return;
        }

        var spawnedPositions = new System.Collections.Generic.List<Vector3>();

        for (int i = 0; i < numberOfObjects; i++)
        {
            int attempts = 0;
            bool validPositionFound = false;

            while (attempts < maxSpawnAttempts && !validPositionFound)
            {
                Vector2 randomCirclePoint = Random.insideUnitCircle * (maxSpawnRadius - minSpawnRadius);

                Vector3 randomOffset = new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);
                Vector3 potentialSpawnPosition = playerTransform.position + randomOffset;

                if (Vector3.Distance(potentialSpawnPosition, playerTransform.position) < minSpawnRadius)
                {
                    attempts++;
                    continue;
                }

                bool tooCloseToOthers = false;
                foreach (Vector3 spawnedPos in spawnedPositions)
                {
                    if (Vector3.Distance(potentialSpawnPosition, spawnedPos) < minDistanceBetweenObjects)
                    {
                        tooCloseToOthers = true;
                        break;
                    }
                }

                if (tooCloseToOthers)
                {
                    attempts++;
                    continue;
                }

                if (spawnTerrain != null)
                {
                    float terrainHeight = spawnTerrain.SampleHeight(potentialSpawnPosition);
                    potentialSpawnPosition.y = spawnTerrain.transform.position.y + terrainHeight;
                }


                GameObject spawnedObject = Instantiate(
                    objectToSpawn,
                    potentialSpawnPosition,
                    Quaternion.Euler(0, Random.Range(0f, 360f), 0)
                );

                spawnedObject.transform.SetParent(transform);

                spawnedPositions.Add(potentialSpawnPosition);

                validPositionFound = true;
            }

            if (!validPositionFound)
            {
                Debug.LogWarning($"Could not find a valid spawn point for object {i}");
            }
        }
    }
}