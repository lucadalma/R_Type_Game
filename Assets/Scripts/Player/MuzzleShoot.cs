using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleShoot : MonoBehaviour
{

    [SerializeField] GameObject FastBullet;
    [SerializeField] float FastBulletSpeed = 10f;


    [SerializeField] GameObject SlowBullet;
    [SerializeField] float SlowBulletSpeed = 5f;


    private List<GameObject> bulletsSlowShoot = new List<GameObject>();
    private List<GameObject> bulletsFastShoot = new List<GameObject>();

    PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        ShootFastBullet();
        
        if (playerController.stamina >= 1) 
        {
            ShootSlowBullet();
        }

        foreach (GameObject bulletFastShoot in bulletsFastShoot)
        {
            if (bulletFastShoot != null) 
            {
                bulletFastShoot.transform.Translate(Vector2.right * FastBulletSpeed * Time.deltaTime, Space.Self);
            }
        }

        foreach (GameObject bulletSlowShoot in bulletsSlowShoot)
        {
            if (bulletSlowShoot != null)
            {
                bulletSlowShoot.transform.Translate(Vector2.right * SlowBulletSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    private void ShootFastBullet()
    {
        bool shoot = Input.GetKeyDown("q");
        if (shoot)
        {
            bulletsFastShoot.Add(Instantiate(FastBullet, transform.position, Quaternion.identity));

        }
    }

    private void ShootSlowBullet()
    {
        bool shoot = Input.GetKeyDown("e");
        if (shoot)
        {
            bulletsSlowShoot.Add(Instantiate(SlowBullet, transform.position, Quaternion.identity));
            playerController.ConsumeStaminaAll();
        }

    }

}
