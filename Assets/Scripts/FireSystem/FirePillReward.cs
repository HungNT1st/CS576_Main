using UnityEngine;

public class FirePillReward : MonoBehaviour
{
    [System.Serializable]
    public class PillData
    {
        public GameObject prefab;
        [Range(0f, 1f)] public float spawnChance = 0.5f;
    }

    [SerializeField] private PillData redPill;
    [SerializeField] private PillData bluePill;
    [SerializeField] private float spawnHeight = 0.5f;
    //[SerializeField] private ParticleSystem spawnEffect;

    public void SpawnReward()
    {
        Debug.Log("SpawnReward called - Attempting to spawn pill");
        
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("SpawnReward failed - Player not found!");
            return;
        }

        // Check if pill prefabs are assigned
        if (redPill.prefab == null || bluePill.prefab == null)
        {
            Debug.LogError("SpawnReward failed - Pill prefabs not assigned in inspector!");
            return;
        }

        float totalChance = redPill.spawnChance + bluePill.spawnChance;
        float randomValue = Random.Range(0f, totalChance);
        
        GameObject pillToSpawn = null;
        string pillColor = "";
        
        if (randomValue <= redPill.spawnChance)
        {
            pillToSpawn = redPill.prefab;
            pillColor = "Red";
        }
        else
        {
            pillToSpawn = bluePill.prefab;
            pillColor = "Blue";
        }

        Debug.Log($"Selected {pillColor} pill to spawn (Random value: {randomValue}/{totalChance})");

        if (pillToSpawn != null)
        {
            // Spawn at player's position
            Vector3 spawnPosition = player.transform.position + Vector3.up * spawnHeight;
            GameObject spawnedPill = Instantiate(pillToSpawn, spawnPosition, Quaternion.identity);
            Debug.Log($"Spawned {pillColor} pill at position: {spawnPosition}");
        }
    }
} 