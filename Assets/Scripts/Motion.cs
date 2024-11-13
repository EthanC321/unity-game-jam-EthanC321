using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public CharacterController controller;

    #region Variables
    public float speed = 5;
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float gravity = -9.81f * 2.0f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    public PlayerStats playerStats;
    private float staminaRegenDelay = 4.0f;
    private float staminaRegenTimer = 0f;

    private float staminaDepletionTimer = 0f;
    public float staminaDepletionInterval = .5f; 
    public int staminaDepletionAmount = 5;
    #endregion

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        bool isSprinting = z > 0 && Input.GetKey(KeyCode.LeftShift);

        if (isSprinting && playerStats.currentStamina > 0)
        {
            speed = runSpeed;
            staminaRegenTimer = staminaRegenDelay;

            staminaDepletionTimer += Time.deltaTime;
            if (staminaDepletionTimer >= staminaDepletionInterval)
            {
                playerStats.currentStamina -= staminaDepletionAmount;
                staminaDepletionTimer = 0f;
            }
        }
        else
        {
            speed = walkSpeed;

            if (staminaRegenTimer > 0)
            {
                staminaRegenTimer -= Time.deltaTime;
            }
            else if (playerStats.currentStamina < playerStats.maxStamina)
            {
                playerStats.currentStamina += 1;
            }

            staminaDepletionTimer = 0f; 
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
