using UnityEngine;
using System.Collections;

public class FireSpawner : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private int maxFiresAtOnce = 3;
    [SerializeField] private int initialFireCount = 5;
    
    private void Start()
    {
        SpawnInitialFires();
        
        StartCoroutine(SpawnFireRoutine());
    }
    
    private void SpawnInitialFires()
    {
        for (int i = 0; i < initialFireCount; i++)
        {
            SpawnFireOnRandomTree();
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
        
        // Spawn fire at tree position with 270-degree rotation on X axis
        Vector3 spawnPos = selectedTree.transform.position;
        Quaternion rotation = Quaternion.Euler(270f, 0f, 0f);
        GameObject fire = Instantiate(firePrefab, spawnPos, rotation);
        fire.transform.SetParent(selectedTree.transform);
        fire.tag = "Fire";
    }
}
