using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TreePlantingHandler : MonoBehaviour
{
    [SerializeField] Terrain targetTerrain;

    [SerializeField] Transform treePrefab;

    private Transform playerTransform;

    const int TREEHEIGHT = 5;
    public void SpawnTreeAtPlayerPosition()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
        Vector3 spawnPos = playerTransform.position + playerTransform.forward * 5;
        spawnPos.y = targetTerrain.SampleHeight(spawnPos) - TREEHEIGHT;

        Transform tree = Instantiate(treePrefab, spawnPos, Quaternion.identity);
        tree.DOMoveY(spawnPos.y + TREEHEIGHT, 4f);

        AddTreeToTerrain(spawnPos);
        GameManager.Instance.HealWorld(20);
    }

    private void AddTreeToTerrain(Vector3 position)
    {
        TreePrototype[] treePrototypes = targetTerrain.terrainData.treePrototypes;

        if (treePrototypes.Length == 0)
        {
            Debug.LogWarning("No tree prototypes found in terrain. Add a tree prototype first.");
            return;
        }

        TreeInstance treeInstance = new TreeInstance
        {
            position = targetTerrain.transform.InverseTransformPoint(position),
            prototypeIndex = 0, 
            widthScale = 1f,
            heightScale = 1f,
            color = Color.white,
            lightmapColor = Color.white
        };

        targetTerrain.AddTreeInstance(treeInstance);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            HUD.Instance.SetSmallTaskLoading("Planting", 5).onComplete += SpawnTreeAtPlayerPosition;
        }
        if (Input.GetKeyUp(KeyCode.T)) {
            HUD.Instance.StopSmallTaskLoading();
        }
    }
}