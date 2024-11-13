using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public float Expiry = 4f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Destroy(gameObject, Expiry);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Pellet hit " + collision.gameObject.name + " at " + collision.transform.position);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Zombie"))
        {
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.score += 10; 
                Debug.Log("Score increased! Current score: " + playerStats.score);
            }

            Destroy(collision.gameObject); 
            Debug.Log("Zombie destroyed by pellet!");
        }

        Destroy(gameObject); 
    }
}
