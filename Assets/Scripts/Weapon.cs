using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
   public string weaponName;
    public int damage;
    public float range;
    public float attackRate;

    public Animator animator;

    public abstract void Attack();

    void Start()
    {
        animator = GetComponent<Animator>();
    }
}
