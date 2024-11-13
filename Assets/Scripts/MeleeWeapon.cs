using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public float attackRange = 2.0f; // Range of the melee attack
    public int attackDamage = 10;
    public Animator animator;

    private bool isSwinging = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Attack()
    {
        isSwinging = true;
        animator.SetTrigger("Attack");
        PerformMeleeAttack();
    }

    public void StopSwinging()
    {
        isSwinging = false;
    }

    private void PerformMeleeAttack()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject zombie in zombies)
        {
            float distance = Vector3.Distance(transform.position, zombie.transform.position);
            if (distance <= attackRange)
            {
                PlayerStats playerStats = FindObjectOfType<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.score += 10;
                    Debug.Log("Score increased by 10. Current score: " + playerStats.score);
                }

                Destroy(zombie);
                Debug.Log("Zombie destroyed by melee attack.");
            }
        }
    }
}
