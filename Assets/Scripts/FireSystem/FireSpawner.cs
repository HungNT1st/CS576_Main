using UnityEngine;
using System.Collections;

public class FireSpawner : MonoBehaviour
{
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private float spawnInterval = 10f;
    [SerializeField] private int maxFiresAtOnce = 3;
    
    private void Start()
    {
        StartCoroutine(SpawnFireRoutine());
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
        
        // Spawn fire at tree position
        Vector3 spawnPos = selectedTree.transform.position;
        GameObject fire = Instantiate(firePrefab, spawnPos, Quaternion.identity);
        fire.transform.SetParent(selectedTree.transform);
        fire.tag = "Fire";
    }
}
