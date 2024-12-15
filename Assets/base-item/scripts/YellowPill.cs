using UnityEngine;

public class YellowPill : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 0.5f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        if (player != null)
        {
            // Store original speeds
            float originalWalkSpeed = player.walkSpeed;
            float originalSprintSpeed = player.sprintSpeed;

            // Apply speed reduction
            player.walkSpeed *= speedMultiplier;
            player.sprintSpeed *= speedMultiplier;

            // Reset after duration
            StartCoroutine(ResetSpeedAfterDelay(player, originalWalkSpeed, originalSprintSpeed));

            // Destroy the pill
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator ResetSpeedAfterDelay(FirstPersonController player, float originalWalkSpeed, float originalSprintSpeed)
    {
        yield return new WaitForSeconds(duration);
        
        if (player != null)
        {
            player.walkSpeed = originalWalkSpeed;
            player.sprintSpeed = originalSprintSpeed;
        }
    }
} 