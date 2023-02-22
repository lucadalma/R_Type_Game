using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] float health;
    [SerializeField] float enemySpeed;
    [SerializeField] float enemyDamage;

    [SerializeField] GameObject enemyBullet;
    [SerializeField] float enemySpeedBullet;
    [SerializeField] Transform muzzle;


    [SerializeField] Image LifeBar;
    [SerializeField] float enemyShootRate;

    PlayerController playerController;
    GameObject player;

    Rigidbody2D rigidbody2D;
    private List<GameObject> bulletsEnemy = new List<GameObject>();
    

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

        playerController = FindObjectOfType<PlayerController>();
        player = playerController.gameObject;
    }

    private void Update()
    {
        if (health <= 0) 
        {
            Destroy(gameObject);
        }

        foreach (GameObject bulletEnemy in bulletsEnemy)
        {
            if (bulletEnemy != null)
            {
                bulletEnemy.transform.position = Vector2.MoveTowards(bulletEnemy.transform.position, player.transform.position, enemySpeedBullet * Time.deltaTime);
            }
        }

        MoveEnemy();
        StartCoroutine("EnemyShoot");
    }

    private void MoveEnemy() 
    {
        Vector2 movementEnemy = new Vector2(-1, 0 );
        rigidbody2D.velocity = movementEnemy * enemySpeed * Time.deltaTime;
    }

    private IEnumerable EnemyShoot()
    {
        bulletsEnemy.Add(Instantiate(enemyBullet, muzzle.transform.position, Quaternion.identity));
        Debug.Log("ShootPlayer");
        yield return new WaitForSeconds(1);
    }
}
