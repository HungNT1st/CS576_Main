using UnityEngine;

public class FightVillian : MonoBehaviour
{
    public Animator handAnimator;
    public float detectionRadius = 3.5f;
    public string villianTag = "Villain";
    private bool drawHitbox = false;
    private float hitboxDuration = 1f;
    private float hitboxTimer = 0f;
    private bool fightStarted = false;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayPunchAnimation();
            CheckForVillian();
            ActivateHitbox();
        }

        if (fightStarted)
        {
            AnimatorStateInfo stateInfo = handAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1f)
            {
                PauseGame();
                fightStarted = false;
            }
        }

        if (drawHitbox)
        {
            hitboxTimer -= Time.deltaTime;
            if (hitboxTimer <= 0f)
            {
                drawHitbox = false;
            }
        }
    }

    void PlayPunchAnimation()
    {
        if (handAnimator != null)
        {
            handAnimator.SetTrigger("Punch");
        }
    }

    void CheckForVillian()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag(villianTag))
            {
                fightStarted = true;
                break;
            }
        }
    }

    void ActivateHitbox()
    {
        drawHitbox = true;
        hitboxTimer = hitboxDuration;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void OnDrawGizmos()
    {
        if (drawHitbox)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
