using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trash : MonoBehaviour
{
    [SerializeField] private CoinManager coinManager;
    private void Start() {
        GetComponent<Collider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayAudioGroup("TRASH COLLECT");
        if (other.CompareTag("Player")) {
            HUD.Instance.SetSmallTaskLoading("Cleaning up trash", 4).onComplete += () => {
                Destroy(gameObject);
                GameManager.Instance.HealWorld(5);
                if (coinManager != null) {
                    coinManager.AddCoins(1);
                    HUD.Instance.PopUpText("Trash collected successfully!", 2);
                }
            };
        }
    }
    private void OnTriggerExit(Collider other)
    {
        HUD.Instance.StopSmallTaskLoading();
    }
}
