using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private float activationDelay = 0.5f;
    [SerializeField] private ParticleSystem trapEffect;
    
    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;
            Invoke(nameof(ActivateTrap), activationDelay);
        }
    }

    private void ActivateTrap()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                VillainHealth villainHealth = hitCollider.GetComponent<VillainHealth>();
                if (villainHealth != null)
                {
                    villainHealth.TakeDamage();
                }
            }
        }

        if (trapEffect != null)
        {
            trapEffect.Play();
        }
    }
} 