using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PetsAddingHandler : MonoBehaviour
{
    [SerializeField] private Terrain targetTerrain;

    [SerializeField] private Transform[] petPrefabs; 
    
    [SerializeField] private CoinManager coinManager;

    private Transform playerTransform;

    public void SpawnTreeAtPlayerPosition()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        Vector3 spawnPos = playerTransform.position + playerTransform.forward * 5f;
        spawnPos.y = targetTerrain.SampleHeight(spawnPos);

        if (petPrefabs != null && petPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, petPrefabs.Length);
            Transform chosenPet = petPrefabs[randomIndex];

            // TODO: Play sound when spawning pet
            Transform spawnedPet = Instantiate(chosenPet, spawnPos, Quaternion.identity);
            spawnedPet.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No pet prefabs assigned. Please set them in the inspector.");
        }
        GameManager.Instance.HealWorld(15);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (coinManager != null && coinManager.GetCoins() >= 5)
            {
                HUD.Instance.SetSmallTaskLoading("Spawning Pets", 5).onComplete += () =>
                {
                    SpawnTreeAtPlayerPosition(); 
                    coinManager.RemoveCoins(5);
                };
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            HUD.Instance.StopSmallTaskLoading();
        }
    }
}
