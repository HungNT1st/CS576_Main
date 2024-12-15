using UnityEngine;
using System.Collections;

public class FireManager : MonoBehaviour
{
    [Header("Fire Settings")]
    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private float extinguishDelay = 1f;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageInterval = 0.5f;
    [SerializeField] private float waterNeeded = 3f;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem steamEffect;
    
    private bool isExtinguishing = false;
    private bool isExtinguished = false;
    private FirePillReward pillReward;
    private float nextDamageTime = 0f;
    private float waterAccumulated = 0f;
    private GameManager gameManager;

    private void Start()
    {
        // Get references
        if (fireParticleSystem == null)
        {
            fireParticleSystem = GetComponent<ParticleSystem>();
        }
        
        pillReward = GetComponent<FirePillReward>();
        gameManager = FindObjectOfType<GameManager>();
        
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }

        // Make sure fire is playing
        if (fireParticleSystem != null && !fireParticleSystem.isPlaying)
        {
            fireParticleSystem.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isExtinguished) return;

        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            if (gameManager != null)
            {
                gameManager.DamagePlayer(damageAmount);
                nextDamageTime = Time.time + damageInterval;
                Debug.Log($"Player damaged for {damageAmount}");
            }
        }
    }

    public void ApplyWater(float amount)
    {
        if (isExtinguished || isExtinguishing) return;
        
        // Accumulate water
        waterAccumulated += amount;
        Debug.Log($"Water accumulated: {waterAccumulated}/{waterNeeded}");
        
        // Play steam effect when water hits fire
        if (steamEffect != null && !steamEffect.isPlaying)
        {
            steamEffect.Play();
        }

        // Start extinguishing when enough water is accumulated
        if (waterAccumulated >= waterNeeded)
        {
            StartCoroutine(ExtinguishFireRoutine());
        }
    }

    private IEnumerator ExtinguishFireRoutine()
    {
        isExtinguishing = true;
        
        // Gradually reduce particle emission
        if (fireParticleSystem != null)
        {
            var emission = fireParticleSystem.emission;
            var startRate = emission.rateOverTime.constant;
            float elapsedTime = 0f;
            
            while (elapsedTime < extinguishDelay)
            {
                float newRate = Mathf.Lerp(startRate, 0f, elapsedTime / extinguishDelay);
                emission.rateOverTime = newRate;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        
        ExtinguishFire();
    }

    private void ExtinguishFire()
    {
        isExtinguished = true;

        // Stop fire particles
        if (fireParticleSystem != null)
        {
            fireParticleSystem.Stop();
        }

        // Spawn reward if available
        if (pillReward != null)
        {
            pillReward.SpawnReward();
        }

        // Optionally destroy the fire object after a delay
        Destroy(gameObject, 2f);
    }
} 