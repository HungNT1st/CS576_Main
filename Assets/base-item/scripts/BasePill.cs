using UnityEngine;

public abstract class BasePill : BaseItem
{
    [Header("Pill Settings")]
    [SerializeField] protected float duration = 10f;
    [SerializeField] protected ParticleSystem consumeEffect;
    
    protected bool isActive = false;
    protected float activeTimer = 0f;

    public override bool Use()
    {
        if (!base.Use()) return false;
        
        ActivateEffect();
        if (consumeEffect != null)
        {
            consumeEffect.Play();
        }
        
        return true;
    }

    protected virtual void ActivateEffect()
    {
        isActive = true;
        activeTimer = duration;
    }

    protected virtual void DeactivateEffect()
    {
        isActive = false;
    }

    protected override void Update()
    {
        base.Update();
        
        if (isActive)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0)
            {
                DeactivateEffect();
            }
        }
    }
} 