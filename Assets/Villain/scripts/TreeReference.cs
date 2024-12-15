using UnityEngine;
using System.Collections.Generic;

public class TreeReference : MonoBehaviour
{
    public int TreeIndex { get; set; }

    public void RemoveTree()
    {
        // Get the terrain and remove the tree
        Terrain terrain = Terrain.activeTerrain;
        if (terrain != null)
        {
            TreeInstance[] trees = terrain.terrainData.treeInstances;
            List<TreeInstance> newTrees = new List<TreeInstance>(trees);
            
            if (TreeIndex < newTrees.Count)
            {
                newTrees.RemoveAt(TreeIndex);
                terrain.terrainData.treeInstances = newTrees.ToArray();
            }
        }
        
        // Destroy the collider object
        Destroy(gameObject);
    }
}
