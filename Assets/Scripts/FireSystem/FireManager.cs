using UnityEngine;
using System.Collections;

public class FireManager : MonoBehaviour
{
    [Header("Fire Settings")]
    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private float waterRequiredToExtinguish = 5f;
    [SerializeField] private float extinguishDelay = 3f;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem steamEffect;
    
    private float currentWaterAmount = 0f;
    private bool isExtinguishing = false;
    private bool isExtinguished = false;
    private FirePillReward pillReward;

    private void Start()
    {
        if (fireParticleSystem == null)
        {
            fireParticleSystem = GetComponent<ParticleSystem>();
        }
        
        pillReward = GetComponent<FirePillReward>();
    }

    public void ApplyWater(float amount)
    {
        if (isExtinguished || isExtinguishing) return;

        currentWaterAmount += amount;
        
        // Play steam effect when water hits fire
        if (steamEffect != null && !steamEffect.isPlaying)
        {
            steamEffect.Play();
        }

        if (currentWaterAmount >= waterRequiredToExtinguish && !isExtinguishing)
        {
            StartCoroutine(ExtinguishFireRoutine());
        }
    }

    private IEnumerator ExtinguishFireRoutine()
    {
        isExtinguishing = true;
        
        // Reduce fire particle effect gradually
        var emission = fireParticleSystem.emission;
        float startRate = emission.rateOverTime.constant;
        
        float elapsedTime = 0f;
        while (elapsedTime < extinguishDelay)
        {
            float t = elapsedTime / extinguishDelay;
            emission.rateOverTime = startRate * (1f - t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        ExtinguishFire();
    }

    private void ExtinguishFire()
    {
        isExtinguished = true;

        // Stop fire particles
        if (fireParticleSystem != null)
        {
            var emission = fireParticleSystem.emission;
            emission.enabled = false;
            fireParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }

        // Spawn reward
        if (pillReward != null)
        {
            pillReward.SpawnReward();
        }

        // Cleanup
        Destroy(gameObject, 1f);
    }
} 