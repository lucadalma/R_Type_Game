using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{

    //variabili
    [SerializeField] Transform edge1;
    [SerializeField] Transform edge2;
    [SerializeField] float spawnEnemyRate;
    [SerializeField] float spawnGemRate;
    [SerializeField] public GameObject Boss;
    [SerializeField] public GameObject lifeGem;
    [SerializeField] public Transform bossPosition;
    GameManager gameManager;
    GameObject enemyToSpawn;
    float spawnTimer;
    float spawnGemTimer;
    float upLimit, downLimit;
    int numberBoss = 0;
    List<GameObject> enemys = new List<GameObject>();


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();


        upLimit = edge1.position.y;
        downLimit = edge2.position.y;
    }

    void Update()
    {
        //Codice spawn in base alla wave
        if (gameManager.wave == GameManager.Waves.Wave1)
        {
            //cambio l'enemy to spawn in base alla wave
            enemyToSpawn = gameManager.enemyWave1;
        }
        else if (gameManager.wave == GameManager.Waves.Wave2)
        {
            spawnEnemyRate = 1.1f;
            enemyToSpawn = gameManager.enemyWave2;
        }
        else if (gameManager.wave == GameManager.Waves.Wave3)
        {
            spawnEnemyRate = 1.5f;
            enemyToSpawn = gameManager.enemyWave3;
        }
        else if (gameManager.wave == GameManager.Waves.Boss) 
        {
            spawnEnemyRate += Time.time;
            if (numberBoss == 0) 
            {
                SpawnBoss();
                numberBoss++;
            }
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnEnemyRate)
        {
            SpawnEnemys();
        }

        spawnGemTimer += Time.deltaTime;

        if (spawnGemTimer >= spawnGemRate) 
        {
            SpawnGem();
        }
    }

    
    private void SpawnEnemys() 
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(upLimit, downLimit));
        enemys.Add(Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity));
        spawnTimer = 0;
    }

    private void SpawnBoss() 
    {
        Vector2 spawnPosition = new Vector2(bossPosition.position.x, bossPosition.position.y);
        GameObject boss = Instantiate(Boss, spawnPosition, Quaternion.identity);
        foreach (GameObject enemy in enemys)
        {
            Destroy(enemy);
        }
    }

    //metodo per spawnare la life gem
    private void SpawnGem()
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(upLimit, downLimit));
        GameObject gem = Instantiate(lifeGem, spawnPosition, Quaternion.identity);
        spawnGemTimer = 0;
    }
}
