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
        StartCoroutine(coroutine);
    }

    private void Update()
    {
        if (health <= 0) 
        {
            gameManager.killCount += 1;
            Debug.Log(gameManager.killCount);
            Destroy(gameObject);
        }

        //foreach (GameObject bulletEnemy in bulletsEnemy)
        //{
        //    if (bulletEnemy != null)
        //    {
        //        bulletEnemy.transform.position = Vector2.MoveTowards(bulletEnemy.transform.position, player.transform.position, enemySpeedBullet * Time.deltaTime);

        //    }
        //}

        if (movementEnemy == Movement.Straight)
        {
            MoveEnemyStraight();
        } 
        else if (movementEnemy == Movement.ZigZag) 
        {
            MoveEnemyZigZag();
        }
        

    }

    private void MoveEnemyStraight() 
    {
        Vector2 movementEnemy = new Vector2(-1, 0 );
        rigidbody2D.velocity = movementEnemy * enemySpeed * Time.deltaTime;
    }

    private void MoveEnemyZigZag()
    {
        Vector2 movementEnemy = new Vector2(-1, 0) * enemySpeed * Time.deltaTime;
        rigidbody2D.velocity = movementEnemy + new Vector2(0,1) * Mathf.Sin(Time.time * 2) * 1;
    }

    IEnumerator EnemyShoot(float _enemyShootRate)
    {
        for (; ; )
        {
            Instantiate(enemyBullet, muzzle.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_enemyShootRate);
        }
    }
}
