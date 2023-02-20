using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float healthPlayer;
    [SerializeField] float speedPlayer = 500f;
    [SerializeField] float sprintSpeedPlayer = 1000f;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletDamage;
    public float stamina = 100f;

    Rigidbody2D rigidbody2D;

    private void Start()
    {
        try
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
        catch (System.Exception)
        {
            Debug.LogError("RigidBody mancante");
        }

    }

    private void Update()
    {
        MovePlayer();

        if (stamina < 100) 
        {
            RestoreStamina();
        }
    }

    public void MovePlayer()
    {
        if (Input.GetButton("Sprint") && stamina > 0)
        {

            Debug.Log("Sprint");
            float horizontalSprint = Input.GetAxisRaw("Horizontal") * sprintSpeedPlayer;
            float verticalSprint = Input.GetAxisRaw("Vertical") * sprintSpeedPlayer;

            Vector2 movementPlayerSprint = new Vector2(horizontalSprint, verticalSprint);

            rigidbody2D.velocity = movementPlayerSprint * Time.deltaTime;
        }
        else 
        {
            Debug.Log("Normal");
            float horizontal = Input.GetAxisRaw("Horizontal") * speedPlayer;
            float vertical = Input.GetAxisRaw("Vertical") * speedPlayer;

            Vector2 movementPlayer = new Vector2(horizontal, vertical);

            rigidbody2D.velocity = movementPlayer * Time.deltaTime;
        }

    }


    private void Shoot() 
    {
        bool shoot = Input.GetButtonDown("Jump");
        
    }

    private void ConsumeStamina() 
    {
        
    }

    private void RestoreStamina()
    {

    }

}
