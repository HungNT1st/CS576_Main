using UnityEngine;
using System.Collections;

public class VillainBehavior : MonoBehaviour
{
    public GameManager gameManager;
    // Set tree dmg here  
    public float treeDmg = 0.5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float attackCooldown = 1f;
    
    private Transform currentTargetTree;
    private int currentTreeHealth = 10;
    private int health;
    private bool isAttacking = false;
    private VillainAnimationController animController;
    private bool isDead = false;

    private void Start()
    {
        health = maxHealth;
        animController = GetComponent<VillainAnimationController>();
    }

    public void Initialize(Transform initialTree)
    {
        currentTargetTree = initialTree;
        FaceTarget();
        StartCoroutine(AIRoutine());
    }

    private IEnumerator AIRoutine()
    {
        while (!isDead)
        {
            if (currentTargetTree == null)
            {
                FindNextTree();
                yield return null;
                continue;
            }

            // Move towards tree if not in range
            float distanceToTree = Vector3.Distance(transform.position, currentTargetTree.position);
            if (distanceToTree > attackRange)
            {
                MoveTowardsTree();
            }
            else if (!isAttacking)
            {
                StartCoroutine(AttackTree());
            }

            yield return null;
        }
    }

    private void MoveTowardsTree()
    {
        Vector3 direction = (currentTargetTree.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);
        
        // Update animation to running state
        if (animController != null)
        {
            animController.SetState(VillainAnimationController.States.Running);
        }
    }

    private IEnumerator AttackTree()
    {
        isAttacking = true;
        
        while (currentTargetTree != null && !isDead)
        {
            // Face the tree while attacking
            FaceTarget();
            
            // Set attack state
            if (animController != null)
            {
                animController.SetState(VillainAnimationController.States.Attack);
            }

            // Wait for attack animation to complete
            yield return new WaitForSeconds(animController.GetClipLength("AttackHor"));

            // Deal damage
            currentTreeHealth--;
            if (currentTreeHealth <= 0 && currentTargetTree != null)
            {
                TreeReference treeRef = currentTargetTree.GetComponent<TreeReference>();
                if (treeRef != null)
                {
                    treeRef.RemoveTree();
                    if (gameManager != null)
                    {
                        gameManager.DamageWorld(treeDmg);  
                    }
                }
                currentTargetTree = null;
                // Return to idle state when target is destroyed
                if (animController != null)
                {
                    animController.SetState(VillainAnimationController.States.Idle);
                }
                break;
            }

            // Brief pause between attacks
            yield return new WaitForSeconds(attackCooldown);
            
            // Reset to attack state for next attack
            if (animController != null && currentTargetTree != null)
            {
                animController.SetState(VillainAnimationController.States.Attack);
            }
        }
        
        isAttacking = false;
    }

    private void FindNextTree()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        float closestDistance = float.MaxValue;
        Transform closestTree = null;

        foreach (GameObject tree in trees)
        {
            float distance = Vector3.Distance(transform.position, tree.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTree = tree.transform;
            }
        }

        currentTargetTree = closestTree;
        if (closestTree != null)
        {
            currentTreeHealth = 10; // Reset health for new tree
        }
    }

    public void TakeDamage()
    {
        health--;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        if (animController != null)
        {
            animController.SetState(VillainAnimationController.States.Death);
        }
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        // Wait for death animation to play
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void FaceTarget()
    {
        if (currentTargetTree != null)
        {
            Vector3 direction = (currentTargetTree.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
