using UnityEngine;

public class RedPill : MonoBehaviour
{
    [SerializeField] private float healthChange = 25f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        if (player != null)
        {
            // 50-50 chance to heal or damage
            bool isHealing = Random.value > 0.5f;
            
            if (isHealing)
            {
                GameManager.Instance.HealPlayer(healthChange);
                HUD.Instance.PopUpText("Took a pill... Health increased!", 2);
            }
            else
            {
                GameManager.Instance.DamagePlayer(healthChange);
                HUD.Instance.PopUpText("Took a pill... Health decreased!", 2);
            }

            AudioManager.Instance.PlayAudioGroup("PILL COLLECT");

            // Destroy the pill
            Destroy(gameObject);
        }
    }
} 