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
            HUD.Instance.SetSmallTaskLoading("Collecting trash", 4).onComplete += () => {
                Destroy(gameObject);
            };
        }
    }
    private void OnTriggerExit(Collider other)
    {
        HUD.Instance.StopSmallTaskLoading();
    }
}
