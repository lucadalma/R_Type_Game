using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float health;
    [SerializeField] float enemySpeed;

    [SerializeField] GameObject enemyBullet;
    [SerializeField] Transform muzzle;


    [SerializeField] Image LifeBar;
    [SerializeField] float enemyShootRate;


    Rigidbody2D rigidbody2D;
    GameManager gameManager;

    private IEnumerator coroutine;

    public enum Movement 
    {
        Straight,
        ZigZag
    }

    public Movement movementEnemy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //controllo se colpito da un proiettiles
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject bullet = collision.gameObject;
            BulletStats bulletStats = bullet.GetComponent<BulletStats>();
            
            health -= bulletStats.bulletDamage;
            LifeBar.fillAmount = health / 100;
            Destroy(collision.gameObject);

        }

    }

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        coroutine = EnemyShoot(enemyShootRate);
        //shoot dell'enemy
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        //controllo se l'enemy è stato ucciso
        if (health <= 0) 
        {
            gameManager.killCount += 1;
            Debug.Log(gameManager.killCount);
            Destroy(gameObject);
        }

        //due metodi di movimento
        if (movementEnemy == Movement.Straight)
        {
            MoveEnemyStraight();
        } 
        else if (movementEnemy == Movement.ZigZag) 
        {
            MoveEnemyZigZag();
        }
        

    }
    //movimento dritto
    private void MoveEnemyStraight() 
    {
        Vector2 movementEnemy = new Vector2(-1, 0 );
        rigidbody2D.velocity = movementEnemy * enemySpeed;
    }

    //movimento zig zag
    private void MoveEnemyZigZag()
    {
        Vector2 movementEnemy = new Vector2(-1, 0) * enemySpeed;
        rigidbody2D.velocity = movementEnemy + new Vector2(0,1) * Mathf.Sin(Time.time * 2) * 1;
    }

    //metodo per il shooting dell'enemy
    IEnumerator EnemyShoot(float _enemyShootRate)
    {
        for (; ; )
        {
            Instantiate(enemyBullet, muzzle.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_enemyShootRate);
        }
    }
}
