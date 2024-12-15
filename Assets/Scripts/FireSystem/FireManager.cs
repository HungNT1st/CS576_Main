using UnityEngine;
using System.Collections;

public class FireManager : MonoBehaviour
{
    [Header("Fire Settings")]
    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private float extinguishDelay = 3f;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem steamEffect;
    
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
        
        // Play steam effect when water hits fire
        if (steamEffect != null && !steamEffect.isPlaying)
        {
            steamEffect.Play();
        }

        // Start extinguishing immediately when hit by water
        StartCoroutine(ExtinguishFireRoutine());
    }

    private IEnumerator ExtinguishFireRoutine()
    {
        isExtinguishing = true;
        
        // Wait for delay
        yield return new WaitForSeconds(extinguishDelay);
        
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

        // Spawn reward
        if (pillReward != null)
        {
            pillReward.SpawnReward();
        }

        // Cleanup
        Destroy(gameObject, 1f);
    }
} 