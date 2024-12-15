// using UnityEngine;

// public class FireManager : MonoBehaviour
// {
//     [Header("Fire Settings")]
//     [SerializeField] private ParticleSystem fireParticleSystem;
//     [SerializeField] private float waterRequiredToExtinguish = 5f;
//     [SerializeField] private float extinguishTime = 2f;
    
//     [Header("Effects")]
//     [SerializeField] private ParticleSystem steamEffect;
    
//     private float currentWaterAmount = 0f;
//     private bool isExtinguished = false;

//     private FirePillReward pillReward;

//     private void Start()
//     {
//         if (fireParticleSystem == null)
//         {
//             fireParticleSystem = GetComponent<ParticleSystem>();
//         }
        
//         pillReward = GetComponent<FirePillReward>();
//     }

//     public void ApplyWater(float amount)
//     {
//         if (isExtinguished) return;

//         currentWaterAmount += amount;
        
//         // Play steam effect when water hits fire
//         if (steamEffect != null && !steamEffect.isPlaying)
//         {
//             steamEffect.Play();
//         }

//         if (currentWaterAmount >= waterRequiredToExtinguish)
//         {
//             ExtinguishFire();
//         }
//     }

//     private void ExtinguishFire()
//     {
//         isExtinguished = true;

//         // Stop fire particles
//         if (fireParticleSystem != null)
//         {
//             var emission = fireParticleSystem.emission;
//             emission.enabled = false;
//             fireParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
//         }

//         // Spawn reward
//         if (pillReward != null)
//         {
//             pillReward.SpawnReward();
//         }

//         // Cleanup
//         Destroy(gameObject, extinguishTime);
//     }
// } 