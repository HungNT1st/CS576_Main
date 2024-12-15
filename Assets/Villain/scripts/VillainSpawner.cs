using UnityEngine;
using System.Collections.Generic;

public class VillainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject villainPrefab;
    [SerializeField] private GameObject treeColliderPrefab;
    [SerializeField] private int numberOfVillains = 50;
    [SerializeField] private float spawnRadius = 100f;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private float minDistanceFromTrees = 2f;

    private List<GameObject> treeColliders = new List<GameObject>();
    private Terrain terrain;
    private TreeInstance[] trees;

    private void Start()
    {
        InitializeTreeColliders();
        SpawnVillains();
    }

    private void Update()
    {
        terrain = Terrain.activeTerrain;
        trees = terrain.terrainData.treeInstances;
        Debug.Log($"Found {trees.Length} trees on terrain");
    }

    private void InitializeTreeColliders()
    {
        terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("No active terrain found!");
            return;
        }

        trees = terrain.terrainData.treeInstances;
        Debug.Log($"Found {trees.Length} trees on terrain");

        // Create colliders for each tree
        for (int i = 0; i < trees.Length; i++)
        {
            GameObject treeCollider = Instantiate(treeColliderPrefab);
            
            // Calculate world position of the tree
            Vector3 treePosition = Vector3.Scale(trees[i].position, terrain.terrainData.size) + terrain.transform.position;
            treeCollider.transform.position = treePosition;
            treeCollider.transform.SetParent(transform);
            treeCollider.tag = "Tree"; // Set the tag for the collider
            
            // Store tree index in the collider
            TreeReference treeRef = treeCollider.AddComponent<TreeReference>();
            treeRef.TreeIndex = i;
            
            treeColliders.Add(treeCollider);
        }
    }

    private void SpawnVillains()
    {
        if (treeColliders.Count == 0)
        {
            Debug.LogError("No tree colliders available for spawning villains!");
            return;
        }

        List<GameObject> availableTrees = new List<GameObject>(treeColliders);

        for (int i = 0; i < Mathf.Min(numberOfVillains, availableTrees.Count); i++)
        {
            // Pick a random tree
            int randomTreeIndex = Random.Range(0, availableTrees.Count);
            GameObject targetTree = availableTrees[randomTreeIndex];
            availableTrees.RemoveAt(randomTreeIndex);

            // Spawn villain near the tree
            Vector3 spawnPosition = targetTree.transform.position + (Random.insideUnitSphere * minDistanceFromTrees);
            spawnPosition.y = GetTerrainHeight(spawnPosition);

            GameObject villain = Instantiate(villainPrefab, spawnPosition, Quaternion.identity);
            VillainBehavior villainBehavior = villain.GetComponent<VillainBehavior>();
            if (villainBehavior != null)
            {
                villainBehavior.Initialize(targetTree.transform);
            }

        }
    }

    private float GetTerrainHeight(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 100f, Vector3.down, out hit, Mathf.Infinity, terrainLayer))
        {
            return hit.point.y;
        }
        return terrain.SampleHeight(position);
    }
}
