using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float healthPlayer;
    [SerializeField] float speedPlayer = 500f;
    [SerializeField] float sprintSpeedPlayer = 1000f;
    [SerializeField] Image StaminaBar;
    public float stamina = 1f;

    Rigidbody2D rigidbody2D;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet")) 
        {
            BulletStats bulletEnemy = collision.gameObject.GetComponent<BulletStats>();
            healthPlayer -= bulletEnemy.bulletDamage;
            Destroy(collision.gameObject);
        }
    }

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
        if (stamina < 1 && !Input.GetButton("Sprint")) 
        {
            RestoreStamina();
            //Debug.Log("Recuperando Stamina: " + stamina);
        }

        if (healthPlayer <= 0) 
        {
            //GameOver
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
            ConsumeStamina(0.8f);
            //Debug.Log("Consumando Stamina: " + stamina);
        }
        else 
        {
            //Debug.Log("Normal");
            float horizontal = Input.GetAxisRaw("Horizontal") * speedPlayer;
            float vertical = Input.GetAxisRaw("Vertical") * speedPlayer;

            Vector2 movementPlayer = new Vector2(horizontal, vertical);

            rigidbody2D.velocity = movementPlayer * Time.deltaTime;
        }

    }

    public void ConsumeStamina(float value) 
    {
        stamina -= value * Time.deltaTime;
        StaminaBar.fillAmount = stamina;
    }

    public void ConsumeStaminaAll() 
    {
        stamina = 0;
        StaminaBar.fillAmount = stamina;
    }

    private void RestoreStamina()
    {
        stamina += 0.4f * Time.deltaTime;
        StaminaBar.fillAmount = stamina;
    }

}
