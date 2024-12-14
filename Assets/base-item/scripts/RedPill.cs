using UnityEngine;

public class RedPill : BasePill
{
    [SerializeField] private float healAmount = 25f;

    protected override void ActivateEffect()
    {
        base.ActivateEffect();
        
        PlayerHealth playerHealth = GetComponentInParent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
        }
    }
} 