using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum Waves 
    {
        Wave1,
        Wave2,
        Wave3,
        Boss
    }

    public enum GameStatus
    {
        MenuStart,
        GameRunning,
        GameWave,
        GamePause,
        GameEnd
    }

    public enum GameResult 
    {
        playerWin,
        playerLose
    }

    [SerializeField] public GameObject enemyWave1;
    [SerializeField] public GameObject enemyWave2;
    [SerializeField] public GameObject enemyWave3;
    UIManager uIManager;
    PlayerController playerController;
    public GameStatus gameStatus;
    public GameResult gameResult;
    public Waves wave;
    public int killCount = 0;


    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        playerController = FindObjectOfType<PlayerController>();
        gameStatus = GameStatus.MenuStart;
        wave = Waves.Wave1;
        uIManager.WaveNumber("Wave 1");
    }

    private void Update()
    {

        uIManager.KillCounterUI(killCount);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameStatus == GameStatus.GamePause)
            {
                gameStatus = GameStatus.GameRunning;
            }
            else if (gameStatus == GameStatus.GameRunning)
            {
                gameStatus = GameStatus.GamePause;
            }
        }


        if (gameStatus == GameStatus.MenuStart)
        {
            uIManager.StartGame();
            Time.timeScale = 0;
        }
        else if (gameStatus == GameStatus.GameRunning)
        {
            uIManager.GameRunning();
            Time.timeScale = 1;
        }
        else if (gameStatus == GameStatus.GamePause)
        {
            uIManager.PauseGame();
            Time.timeScale = 0;
        }
        else if (gameStatus == GameStatus.GameEnd)
        {
            uIManager.EndGame();
            Time.timeScale = 0;
        }
        else if (gameStatus == GameStatus.GameWave)
        {
            Time.timeScale = 1;
        }

        if (killCount == 15)
        {
            uIManager.WaveNumber("Wave 2");
            wave = Waves.Wave2;
            playerController.healthPlayer = 100;
        }
        else if (killCount == 30) 
        {
            uIManager.WaveNumber("Wave 3");
            wave = Waves.Wave3;
            playerController.healthPlayer = 100;
        }
        else if (killCount == 40)
        {
            uIManager.WaveNumber("BOSS");
            wave = Waves.Boss;
            playerController.healthPlayer = 100;
        }




        if (gameResult == GameResult.playerWin)
        {
            uIManager.PlayerWinUI();
        }
        else 
        {
            uIManager.PlayerLoseUI();
        }
    }

    public void GameRunning()
    {
        gameStatus = GameStatus.GameRunning;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
