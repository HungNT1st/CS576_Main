using UnityEngine;
using System.Collections.Generic;

public class VillainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject villainPrefab;
    [SerializeField] private int numberOfVillains = 5;
    [SerializeField] private float spawnRadius = 100f;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private float minDistanceFromTrees = 2f;
    
    private void Start()
    {
        SpawnVillains();
    }

    private void SpawnVillains()
    {
        List<Transform> trees = new List<Transform>(GameObject.FindGameObjectsWithTag("Tree").Length);
        foreach (var tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            trees.Add(tree.transform);
        }

        for (int i = 0; i < Mathf.Min(numberOfVillains, trees.Count); i++)
        {
            // Pick a random tree
            int randomTreeIndex = Random.Range(0, trees.Count);
            Transform targetTree = trees[randomTreeIndex];
            trees.RemoveAt(randomTreeIndex);

            // Spawn villain near the tree
            Vector3 spawnPosition = targetTree.position + (Random.insideUnitSphere * minDistanceFromTrees);
            spawnPosition.y = GetTerrainHeight(spawnPosition);

            GameObject villain = Instantiate(villainPrefab, spawnPosition, Quaternion.identity);
            VillainBehavior villainBehavior = villain.GetComponent<VillainBehavior>();
            if (villainBehavior != null)
            {
                villainBehavior.Initialize(targetTree);
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
        return 0f;
    }
}
