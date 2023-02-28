using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float healthPlayer;
    [SerializeField] float speedPlayer = 500f;
    [SerializeField] float sprintSpeedPlayer = 1000f;
    [SerializeField] Image StaminaBar;
    [SerializeField] Image HPBar;
    public float stamina = 1f;

    Rigidbody2D rigidbody2D;
    GameManager gameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ottengo collisione dai proiettili dell'enemy
        if (collision.gameObject.CompareTag("EnemyBullet")) 
        {
            EnemyBullet bulletEnemy = collision.gameObject.GetComponent<EnemyBullet>();
            healthPlayer -= bulletEnemy.bulletDamage;
            HPBar.fillAmount = healthPlayer / 100;
            Destroy(collision.gameObject);
        }
        //ottengo collisione con la lifegem
        else if (collision.gameObject.CompareTag("LifeGem"))
        {
            LifeGem lifeGem = collision.gameObject.GetComponent<LifeGem>();
            healthPlayer += lifeGem.healtRestored;
            if (healthPlayer > 100)
            {
                healthPlayer = 100;
            }
            HPBar.fillAmount = healthPlayer / 100;
            Destroy(collision.gameObject);
        }

    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        //aggiorno la barra della vita
        HPBar.fillAmount = healthPlayer / 100;

        //controllo valore vita
        if (healthPlayer > 100) 
        {
            healthPlayer = 100;
        }
        //muovo il player
        MovePlayer();


        if (stamina < 1 && !Input.GetButton("Sprint")) 
        {
            RestoreStamina();
        }

        //condizione se il player muore
        if (healthPlayer <= 0) 
        {
            gameManager.gameStatus = GameManager.GameStatus.GameEnd;
            gameManager.gameResult = GameManager.GameResult.playerLose;
            Destroy(gameObject);
        }
    }

    public void MovePlayer()
    {
        //Sprint player
        if (Input.GetButton("Sprint") && stamina > 0)
        {

            float horizontalSprint = Input.GetAxisRaw("Horizontal") * sprintSpeedPlayer;
            float verticalSprint = Input.GetAxisRaw("Vertical") * sprintSpeedPlayer;

            Vector2 movementPlayerSprint = new Vector2(horizontalSprint, verticalSprint);

            rigidbody2D.velocity = movementPlayerSprint;
            ConsumeStamina(0.6f);
            //Debug.Log("Consumando Stamina: " + stamina);
        }
        else 
        {
            //movimento normale player
            float horizontal = Input.GetAxisRaw("Horizontal") * speedPlayer;
            float vertical = Input.GetAxisRaw("Vertical") * speedPlayer;

            Vector2 movementPlayer = new Vector2(horizontal, vertical);

            rigidbody2D.velocity = movementPlayer;
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
        stamina += 0.5f * Time.deltaTime;
        StaminaBar.fillAmount = stamina;
    }

}
