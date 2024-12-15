using UnityEngine;
using System.Collections;
using System.Linq;

public class FireSpawner : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private int maxFiresAtOnce = 3;
    [SerializeField] private int initialFireCount = 5;
    [SerializeField] private float minSpawnRadius = 20f;
    [SerializeField] private float maxSpawnRadius = 50f;
    
    private Transform playerTransform;
    private Vector3 initialPlayerPosition;
    
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
            return;
        }
        
        initialPlayerPosition = playerTransform.position;
        SpawnInitialFires();
        StartCoroutine(SpawnFireRoutine());
    }
    
    private void SpawnInitialFires()
    {
        for (int i = 0; i < initialFireCount; i++)
        {
            SpawnFireNearPlayer();
        }
    }
    
    private void SpawnFireNearPlayer()
    {
        GameObject[] allTrees = GameObject.FindGameObjectsWithTag("Tree");
        
        var treesInRange = allTrees.Where(tree => {
            float distance = Vector3.Distance(tree.transform.position, initialPlayerPosition);
            return distance >= minSpawnRadius && distance <= maxSpawnRadius;
        }).ToList();
        
        if (treesInRange.Count == 0) return;
        
        // Try to spawn fires on multiple trees
        for (int i = 0; i < 3; i++) // Try up to 3 trees each time
        {
            GameObject selectedTree = treesInRange[Random.Range(0, treesInRange.Count)];
            
            // Check if tree already has fire
            if (selectedTree.GetComponentInChildren<FireManager>() != null) continue;
            
            // Spawn fire at tree position with rotation
            Vector3 spawnPos = selectedTree.transform.position;
            Quaternion rotation = Quaternion.Euler(270f, 0f, 0f);
            GameObject fire = Instantiate(firePrefab, spawnPos, rotation);
            fire.transform.SetParent(selectedTree.transform);
            fire.tag = "Fire";
        }
    }
    
    private IEnumerator SpawnFireRoutine()
    {
        while (true)
        {
            if (GameObject.FindGameObjectsWithTag("Fire").Length < maxFiresAtOnce)
            {
                SpawnFireOnRandomTree();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    private void SpawnFireOnRandomTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        if (trees.Length == 0) return;
        
        // Get random tree
        GameObject selectedTree = trees[Random.Range(0, trees.Length)];
        
        // Check if tree already has fire
        if (selectedTree.GetComponentInChildren<FireManager>() != null) return;
        
        // Spawn fire at tree position with rotation
        Vector3 spawnPos = selectedTree.transform.position;
        Quaternion rotation = Quaternion.Euler(270f, 0f, 0f);
        GameObject fire = Instantiate(firePrefab, spawnPos, rotation);
        fire.transform.SetParent(selectedTree.transform);
        fire.tag = "Fire";
    }
}
