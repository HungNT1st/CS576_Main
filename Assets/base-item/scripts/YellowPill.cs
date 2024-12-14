using UnityEngine;

public class YellowPill : BasePill
{
    [SerializeField] private float speedMultiplier = 0.5f;

    private void Awake()
    {
        duration = 5f;
    }

    protected override void ActivateEffect()
    {
        base.ActivateEffect();
        
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Debug.Log("Setting speed multiplier to: " + speedMultiplier);
            playerMovement.SetSpeedMultiplier(speedMultiplier);
        }
        else
        {
            Debug.Log("PlayerMovement component not found!");
        }
    }

    protected override void DeactivateEffect()
    {
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(1f);
        }
        base.DeactivateEffect();
    }
} 