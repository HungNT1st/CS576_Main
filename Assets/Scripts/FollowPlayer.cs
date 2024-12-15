using UnityEngine;
using UnityEngine.AI;

public class NPCFollow : MonoBehaviour
{
    public float stopDistance = 5.0f;

    private Transform player;
    private Rigidbody playerRigidbody;
    private NavMeshAgent navMeshAgent;
    private Animator animator; 

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerRigidbody = playerObject.GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("Player not found. Ensure the player GameObject has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player != null && playerRigidbody != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            navMeshAgent.speed = playerRigidbody.velocity.magnitude;

            if (distanceToPlayer > stopDistance)
            {
                navMeshAgent.SetDestination(player.position);
                animator.SetBool("IsMoving", true); 
                Debug.Log("Animator IsMoving: " + animator.GetBool("IsMoving"));
            }
            else
            {
                navMeshAgent.ResetPath();
                animator.SetBool("IsMoving", false); 
                Debug.Log("Animator IsMoving: " + animator.GetBool("IsMoving"));
            }

            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0;
            if (directionToPlayer != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }
}

