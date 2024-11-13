using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public int currentStamina;
    public int maxStamina;
    public bool isDead = false;

    public int score = 0;
    public GameObject deathCanvas;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        maxStamina = 100;
        currentStamina = maxStamina;

        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false);
        }
    }

    void Update()
    {
        CheckHealth();
        CheckStamina();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Player has died.");

        if (deathCanvas != null)
        {
            deathCanvas.SetActive(true);
            scoreText.text = "Score: " + score;
        }
    }

    public void CheckHealth()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth < 0)
            currentHealth = 0;
    }

    public void CheckStamina()
    {
        if (currentStamina > maxStamina)
            currentStamina = maxStamina;

        if (currentStamina < 0)
            currentStamina = 0;
    }
}
