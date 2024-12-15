using UnityEngine;

public class FightVillian : MonoBehaviour
{
    public Animator handAnimator; 
    public float detectionRadius = 2f;
    public string villianTag = "Villain"; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayPunchAnimation();
            CheckForVillian();
        }
    }

    void PlayPunchAnimation()
    {
        if (handAnimator != null)
        {
            handAnimator.SetTrigger("Punch"); 
        }
        else
        {
            Debug.LogWarning("Hand Animator not assigned!");
        }
    }

    void CheckForVillian()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            
            if (collider.gameObject.CompareTag(villianTag))
            {
                Debug.Log("Fight started!");
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
