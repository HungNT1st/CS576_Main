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
    [SerializeField] private ParticleSystem spawnEffect;

    public void SpawnReward()
    {
        float totalChance = redPill.spawnChance + bluePill.spawnChance;
        float randomValue = Random.Range(0f, totalChance);

        GameObject pillToSpawn = null;
        
        if (randomValue <= redPill.spawnChance)
        {
            pillToSpawn = redPill.prefab;
        }
        else
        {
            pillToSpawn = bluePill.prefab;
        }

        if (pillToSpawn != null)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * spawnHeight;
            GameObject spawnedPill = Instantiate(pillToSpawn, spawnPosition, Quaternion.identity);
            
            if (spawnEffect != null)
            {
                Instantiate(spawnEffect, spawnPosition, Quaternion.identity);
            }
        }
    }
} 