using UnityEngine;

public class VillainHealth : MonoBehaviour
{
    private VillainBehavior villainBehavior;

    private void Start()
    {
        villainBehavior = GetComponent<VillainBehavior>();
    }

    public void TakeDamage()
    {
        if (villainBehavior != null)
        {
            villainBehavior.TakeDamage();
        }
    }
}
