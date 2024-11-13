using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public float sightRange = 12f;
    public float attackRange = 2f;
    public float escapeRange = 20f;
    public float hitDistance = 2.25f;
    public bool chasing = false;
    public GameObject player;
    public Animator animator;
    [HideInInspector] public bool isAttacking = false;
    public float attackCooldown = 1.5f; 
    private float attackTimer = 0f; 

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.baseOffset = 1.2f;
        animator = GetComponent<Animator>();
        GetComponent<CapsuleCollider>().center -= new Vector3(0,1f,0);
    }

    void Update()
    {
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                isAttacking = false; 
                attackTimer = 0f; 
            }
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= attackRange && Visible() && !isAttacking)
        {
            LookAtPlayer();
            SetAnimationTrigger("Attack");
            Attack();
        }
        else if (distance <= sightRange && Visible() && !isAttacking)
        {
            SetAnimationTrigger("Walk");
            Chase();
        }
        else if (distance < escapeRange && Visible() && chasing && !isAttacking)
        {
            SetAnimationTrigger("Walk");
            Chase();
        }
        else if (distance > escapeRange)
        {
            SetAnimationTrigger("Stop");
            chasing = false;
            Stop();
        }
    }

    void Attack()
    {
        navMeshAgent.destination = transform.position; 
        isAttacking = true; 

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= hitDistance)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null && !playerStats.isDead)
            {
                playerStats.TakeDamage(10);
                Debug.Log("Zombie hits the player!");
            }
        }
    }

    void Chase()
    {
        navMeshAgent.destination = player.transform.position;
        chasing = true;
    }

    void Stop()
    {
        navMeshAgent.destination = transform.position;
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    bool Visible()
    {
        Vector3 direction = player.transform.position - transform.position;
        float distance = direction.magnitude;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distance, LayerMask.GetMask("Obstacle")))
        {
            return false;
        }
        return true;
    }

    void SetAnimationTrigger(string trigger)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(trigger))
        {
            animator.SetTrigger(trigger);
        }
    }
}
