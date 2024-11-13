using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI health,stamina,inventory,score;
    public PlayerStats stats;
    public Inventory inv;
    void Start()
    {
        stats = GetComponent<PlayerStats>();
        inv = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        health.text = "Health: " + stats.currentHealth + "/" + stats.maxHealth;
        stamina.text = "Stamina:" + stats.currentStamina + "/" + stats.maxStamina;
        inventory.text =  inv.weapons[inv.activeWeaponIndex].name;
        score.text = "Score: " + stats.score;
    }
}
