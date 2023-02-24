using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject slowBossBullet;
    [SerializeField] GameObject FastBossBullet;
    [SerializeField] Image LifeBar;
    [SerializeField] Transform muzzleUp;
    [SerializeField] Transform muzzleDown;
    [SerializeField] float bossShootRate;
    Camera camera;
    Transform bossPosition;

    public float healtBoss = 500;
    float healtBar;




    GameManager gameManager;
    GameObject[] Bullets = new GameObject[2];

    private IEnumerator coroutine1;
    private IEnumerator coroutine2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Hit");
            BulletStats bullet = collision.gameObject.GetComponent<BulletStats>();
            healtBoss -= bullet.bulletDamage;
            LifeBar.fillAmount = healtBoss / healtBar;
            Destroy(collision.gameObject);
        }
    }

    void Start()
    {
        healtBar = healtBoss;
        camera = FindObjectOfType<Camera>();
        gameManager = FindObjectOfType<GameManager>();
        bossPosition = camera.transform.GetChild(0);

        Bullets[0] = FastBossBullet;
        Bullets[1] = slowBossBullet;

        coroutine1 = BossShoot(bossShootRate, muzzleUp);
        coroutine2 = BossShoot(bossShootRate, muzzleDown);

        StartCoroutine(coroutine1);
        StartCoroutine(coroutine2);
    }

    void Update()
    {

        //transform.position = Vector3.MoveTowards(transform.position, bossPosition.position, 1);


        if (healtBoss <= 0) 
        {
            gameManager.gameStatus = GameManager.GameStatus.GameEnd;
            gameManager.gameResult = GameManager.GameResult.playerWin;

        }


    }

    IEnumerator BossShoot(float _bossShootRate, Transform _muzzle)
    {
        for (; ; )
        {
            Instantiate(Bullets[Random.Range(0,2)],_muzzle.position, Quaternion.identity);
            yield return new WaitForSeconds(_bossShootRate);
            Debug.Log("Shoot");
        }
    }
}
