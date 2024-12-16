using UnityEngine;
using System.Collections;

public class BluePill : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.5f;
    private float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        if (player != null)
        {
            // Store original speeds
            float originalWalkSpeed = player.walkSpeed;
            float originalSprintSpeed = player.sprintSpeed;

            // Apply speed boost
            player.walkSpeed *= speedMultiplier;
            player.sprintSpeed *= speedMultiplier;
            HUD.Instance.PopUpText("Took a pill... Walk faster for 5s", 2);

            // Reset after duration
            StartCoroutine(ResetSpeedAfterDelay(player, originalWalkSpeed, originalSprintSpeed));

            // Optionally destroy the pill
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