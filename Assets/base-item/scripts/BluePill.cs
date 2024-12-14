using UnityEngine;

public class BluePill : BasePill
{
    [SerializeField] private float speedMultiplier = 1.5f;

    private void Awake()
    {
        duration = 5f; // Set duration to 5 seconds
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