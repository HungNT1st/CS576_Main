using UnityEngine;

public class RedPill : MonoBehaviour
{
    [SerializeField] private float healthBoost = 25f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        if (player != null)
        {
            // Increase sprint duration temporarily
            float originalSprintDuration = player.sprintDuration;
            player.sprintDuration += healthBoost;
            HUD.Instance.PopUpText("Took a pill... Increase health", 2);

            // Reset after duration
            StartCoroutine(ResetHealthAfterDelay(player, originalSprintDuration));

            // Destroy the pill
            Destroy(gameObject);
        }
    }

    private System.Collections.IEnumerator ResetHealthAfterDelay(FirstPersonController player, float originalSprintDuration)
    {
        yield return new WaitForSeconds(duration);
        
        if (player != null)
        {
            player.sprintDuration = originalSprintDuration;
        }
    }
} 