using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableItem
    {
        public GameObject itemPrefab;
        public float spawnWeight = 1f;
    }

    [Header("Spawn Settings")]
    [SerializeField] private SpawnableItem[] spawnableItems;
    [SerializeField] private int numberOfItemsToSpawn = 100;
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(1000f, 1000f);
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float raycastHeight = 1000f;
    [SerializeField] private float minimumDistanceBetweenItems = 10f;
    [SerializeField] private bool useGridSpawning = true;
    [SerializeField] private int gridDivisions = 10;
    private List<Vector3> spawnedPositions = new List<Vector3>();

    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        if (useGridSpawning)
        {
            SpawnItemsInGrid();
        }
        else
        {
            for (int i = 0; i < numberOfItemsToSpawn; i++)
            {
                SpawnRandomItem();
            }
        }
    }

    private void SpawnItemsInGrid()
    {
        float cellSizeX = spawnAreaSize.x / gridDivisions;
        float cellSizeZ = spawnAreaSize.y / gridDivisions;
        int itemsPerCell = numberOfItemsToSpawn / (gridDivisions * gridDivisions);
        
        for (int x = 0; x < gridDivisions; x++)
        {
            for (int z = 0; z < gridDivisions; z++)
            {
                Vector2 cellCenter = new Vector2(
                    (-spawnAreaSize.x / 2) + (x * cellSizeX) + (cellSizeX / 2),
                    (-spawnAreaSize.y / 2) + (z * cellSizeZ) + (cellSizeZ / 2)
                );
                
                for (int i = 0; i < itemsPerCell; i++)
                {
                    Vector3 randomPosition = transform.position + new Vector3(
                        cellCenter.x + Random.Range(-cellSizeX/3, cellSizeX/3),
                        raycastHeight,
                        cellCenter.y + Random.Range(-cellSizeZ/3, cellSizeZ/3)
                    );
                    
                    RaycastHit hit;
                    if (Physics.Raycast(randomPosition, Vector3.down, out hit, raycastHeight * 2, groundLayer))
                    {
                        GameObject selectedItem = SelectRandomItem();
                        if (selectedItem != null)
                        {
                            Vector3 spawnPosition = hit.point + Vector3.up * 1.0f;
                            Instantiate(selectedItem, spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
                        }
                    }
                }
            }
        }
    }

    private GameObject SelectRandomItem()
    {
        if (spawnableItems.Length == 0) return null;

        float totalWeight = 0;
        foreach (var item in spawnableItems)
        {
            totalWeight += item.spawnWeight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = 0;

        foreach (var item in spawnableItems)
        {
            currentWeight += item.spawnWeight;
            if (randomValue <= currentWeight)
            {
                return item.itemPrefab;
            }
        }

        return spawnableItems[0].itemPrefab;
    }

    private bool IsPositionTooClose(Vector3 position)
    {
        foreach (Vector3 spawnedPosition in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPosition) < minimumDistanceBetweenItems)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw spawn area in editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, 1f, spawnAreaSize.y));
    }

    private void SpawnRandomItem()
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            raycastHeight,
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        RaycastHit hit;
        if (Physics.Raycast(randomPosition, Vector3.down, out hit, raycastHeight * 2, groundLayer))
        {
            GameObject selectedItem = SelectRandomItem();
            if (selectedItem != null)
            {
                Vector3 spawnPosition = hit.point + Vector3.up * 1.0f;
                Instantiate(selectedItem, spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            }
        }
    }
}
