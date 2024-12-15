using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class VillainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject villainPrefab;
    // [SerializeField] private GameObject treeColliderPrefab;
    [SerializeField] private GameObject[] treeColliderPrefab;
    [SerializeField] private int numberOfVillains = 20;
    [SerializeField] private float spawnRadius = 100f;
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private float minDistanceFromTrees = 2f;
    [SerializeField]  private GameObject player;

    private List<GameObject> treeColliders = new List<GameObject>();
    private Terrain terrain;
    private TreeInstance[] trees;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
            return;
        }
        InitializeTreeColliders();
        SpawnVillains();
    }

    // private void Update()
    // {
    //     terrain = Terrain.activeTerrain;
    //     trees = terrain.terrainData.treeInstances;
    //     Debug.Log($"Found {trees.Length} trees on terrain");
    // }

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

        // DO NOT SPAWN 2 TREE AT THE SAME PLACES. Ahh, I hate fixing this. 
        int i = 0;
        foreach (TreeInstance tree in trees.Where(t => t.prototypeIndex >= 0 && t.prototypeIndex <= 3))
        {
            Vector3 treePosition = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;
            GameObject treeCollider = Instantiate(treeColliderPrefab[tree.prototypeIndex], treePosition, Quaternion.identity);
            treeCollider.tag = "Tree";

            CapsuleCollider capsuleCollider = treeCollider.AddComponent<CapsuleCollider>();
            capsuleCollider.height = 8f;
            capsuleCollider.radius = 0.7f;
            // capsuleCollider.isTrigger = true;

            TreeReference treeRef = treeCollider.AddComponent<TreeReference>();
            treeRef.TreeIndex = i;

            treeColliders.Add(treeCollider);
            i++;
        }
        // Done fixing. 
        terrain.treeDistance = 0f;

        // Create colliders for each tree
        // for (int i = 0; i < trees.Length; i++)
        // {
        //     GameObject treeCollider = Instantiate(treeColliderPrefab);
            
        //     // Calculate world position of the tree
        //     Vector3 treePosition = Vector3.Scale(trees[i].position, terrain.terrainData.size) + terrain.transform.position;
        //     treeCollider.transform.position = treePosition;
        //     treeCollider.transform.SetParent(transform);
        //     treeCollider.tag = "Tree"; // Set the tag for the collider
            
        //     // Store tree index in the collider
        //     TreeReference treeRef = treeCollider.AddComponent<TreeReference>();
        //     treeRef.TreeIndex = i;
            
        //     treeColliders.Add(treeCollider);
        // }    
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
            // // Pick a random tree
            // int randomTreeIndex = Random.Range(0, availableTrees.Count);
            // GameObject targetTree = availableTrees[randomTreeIndex];
            // availableTrees.RemoveAt(randomTreeIndex);
            
            float randomValue = Random.value;
            float maxRadius;
            if (randomValue < 0.5f) // 50% 
                maxRadius = 100f;
            else if (randomValue < 0.8f) // 30% 
                maxRadius = 75f;
            else // 20% 
                maxRadius = 150f;

            List<GameObject> treesInRange = availableTrees.Where(tree => 
                Vector2.Distance(
                    new Vector2(tree.transform.position.x, tree.transform.position.z),
                    new Vector2(player.transform.position.x, player.transform.position.z)
                ) <= maxRadius
            ).ToList();

            if (treesInRange.Count == 0)
                treesInRange = availableTrees;

            int randomTreeIndex = Random.Range(0, treesInRange.Count);
            GameObject targetTree = treesInRange[randomTreeIndex];
            availableTrees.Remove(targetTree);

            // Spawn villain near the tree
            Vector3 spawnPosition = targetTree.transform.position + (Random.insideUnitSphere * minDistanceFromTrees);
            spawnPosition.y = GetTerrainHeight(spawnPosition);

            GameObject villain = Instantiate(villainPrefab, spawnPosition, Quaternion.identity);
            VillainBehavior villainBehavior = villain.GetComponent<VillainBehavior>();
            if (villainBehavior != null)
            {
                villainBehavior.Initialize(targetTree.transform);
            }

            float distanceFromPlayer = Vector3.Distance(spawnPosition, player.transform.position);
            // Debug.Log($"Spawned villain {i + 1} at distance {distanceFromPlayer:F2} from player");
        }

        // For testing: teleport player to a random villain

        villainPrefab.SetActive(false);
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
