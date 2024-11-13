using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public int ammoCount;
    public int ammoMax;
    public float reloadTime;
    public float pellets;
    public float spreadAngle;
    public float projectileSpeed;
    public bool canShoot = true;
    public GameObject projectilePrefab; 
    public ParticleSystem blast;

    public override void Attack()
    {
        if (!canShoot)
        {
            Debug.Log("Can't shoot yet, waiting for animation to finish.");
            return;
        }
        canShoot = false;
        if(ammoCount < 0){
            Debug.Log(weaponName + " is out of ammo! Reload needed.");
            return;
        }
        blast.Play();
        Debug.Log(weaponName + " fires a projectile with " + damage + " damage.");
        Shoot();
        animator.SetTrigger("Attack");
        ammoCount--;    
        
    }

    public void Reload()
    {
        canShoot = false;
        if (ammoCount == ammoMax) return;
        Debug.Log("reloading " + weaponName);
        animator.SetTrigger("Reload");
        ammoCount = ammoMax;
        
    }

    private void Shoot(){
        Transform barrelExit = transform.Find("BarrelExit");
        for(int i = 0; i<pellets;i++){
            Vector3 spreadDirection = GetSpread(barrelExit.forward,spreadAngle);
            GameObject pellet = Instantiate(projectilePrefab,barrelExit.position,Quaternion.identity);

            pellet.GetComponent<Rigidbody>().velocity = spreadDirection * projectileSpeed;
        }
    }
    private Vector3 GetSpread(Vector3 forward, float angle)
    {
        Quaternion rotation = Quaternion.Euler(Random.Range(-angle,angle),1.5f * Random.Range(-angle,angle),0f);
        return rotation * forward;
    }

    private void UnlockShooting(){
        canShoot = true;
    }
}
