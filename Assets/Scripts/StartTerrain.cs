using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]

public class StartTerrain : MonoBehaviour
{


    public Terrain backupTerrain;
    void Start()
    {
        if (backupTerrain == null)
        {
            Debug.LogError("Backup terrain is not assigned!");
            return;
        }

        InitializeTerrain();
        backupTerrain.gameObject.SetActive(false);
    }

    private void InitializeTerrain()
    {
        Terrain currentTerrain = GetComponent<Terrain>();

        TerrainData clonedTerrainData = Instantiate(backupTerrain.terrainData);

        currentTerrain.terrainData = clonedTerrainData;

        currentTerrain.detailObjectDistance = backupTerrain.detailObjectDistance;
        currentTerrain.treeDistance = backupTerrain.treeDistance;
        currentTerrain.heightmapPixelError = backupTerrain.heightmapPixelError;
        currentTerrain.basemapDistance = backupTerrain.basemapDistance;

        currentTerrain.transform.position = backupTerrain.transform.position;

        Debug.Log("Terrain initialized with backup terrain data.");
    }
    // Start is called before the first frame update
    // void Start()
    // {
    //     Terrain terrain = GetComponent<Terrain>();
    //     TerrainData terrainData = terrain.terrainData;


    //     // Dangerous code -> Destroy your terrain. 
    //     // Terrain backupTerrain = GameObject.Find("BackUpTerrain").GetComponent<Terrain>();
    //     // TerrainData backupData = backupTerrain.terrainData;

    //     // terrainData.baseMapResolution = backupData.baseMapResolution;
    //     // terrainData.heightmapResolution = backupData.heightmapResolution;
    //     // terrainData.size = backupData.size;
    //     // terrainData.SetHeights(0, 0, backupData.GetHeights(0, 0, backupData.heightmapResolution, backupData.heightmapResolution));
    //     // terrainData.SetAlphamaps(0, 0, backupData.GetAlphamaps(0, 0, backupData.alphamapWidth, backupData.alphamapHeight));

    //     // Debug.Log("Number of trees: " + terrainData.treeInstanceCount);
    //     // GameObject.Find("BackUpTerrain").SetActive(false);


    //     // Mass place trees
    //     // int maxTrees = 12500;
    //     // int currentTreeCount = terrainData.treeInstanceCount;

    //     // while (currentTreeCount < maxTrees)
    //     // {
    //     //     float width = terrainData.size.x;
    //     //     float height = terrainData.size.z;
            
    //     //     for (int i = currentTreeCount; i < maxTrees; i++)
    //     //     {
    //     //         TreeInstance tree = new TreeInstance();
    //     //         Vector3 position = new Vector3(Random.Range(0f, 1f), 0f, Random.Range(0f, 1f));
    //     //         tree.position = position;
    //     //         tree.prototypeIndex = Random.Range(0, terrainData.treePrototypes.Length);
    //     //         tree.widthScale = Random.Range(0.8f, 1.2f);
    //     //         tree.heightScale = Random.Range(0.8f, 1.2f);
    //     //         terrainData.SetTreeInstance(i, tree);
    //     //     }
    //     //     currentTreeCount = Terrain.activeTerrain.terrainData.treeInstanceCount;
    //     // }
    // }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Number of trees: " + GetComponent<Terrain>().terrainData.treeInstanceCount);
    }
}
