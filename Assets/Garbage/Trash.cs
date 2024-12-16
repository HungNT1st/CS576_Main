using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Trash : MonoBehaviour
{
    private void Start() {
        GetComponent<Collider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            HUD.Instance.SetSmallTaskLoading("Cleaning up trash", 4).onComplete += () => {
                Destroy(gameObject);
                GameManager.Instance.HealWorld(5);
                if (CoinManager.Instance != null) 
                {
                    CoinManager.Instance.AddCoins(1);
                }
            };
        }
    }
    private void OnTriggerExit(Collider other)
    {
        HUD.Instance.StopSmallTaskLoading();
    }
}
