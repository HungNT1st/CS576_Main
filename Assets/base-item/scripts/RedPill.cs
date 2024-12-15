using UnityEngine;

public class RedPill : BasePill
{
    [SerializeField] private float healAmount = 25f;

    protected override void ActivateEffect()
    {
        base.ActivateEffect();
        
        VillainHealth villainHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<VillainHealth>();
        if (villainHealth != null)
        {
            villainHealth.TakeDamage();
        }
    }
} 