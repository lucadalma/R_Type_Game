using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Enum per le diverse Waves
    public enum Waves 
    {
        Wave1,
        Wave2,
        Wave3,
        Boss
    }

    //Enum per i vari stati di gioco
    public enum GameStatus
    {
        MenuStart,
        GameRunning,
        GameWave,
        GamePause,
        GameEnd
    }

    //Enum per il risultato del game
    public enum GameResult 
    {
        playerWin,
        playerLose
    }


    //Variabili
    [SerializeField] public GameObject enemyWave1;
    [SerializeField] public GameObject enemyWave2;
    [SerializeField] public GameObject enemyWave3;
    [SerializeField] public int killWave2 = 20;
    [SerializeField] public int killWave3 = 35;
    [SerializeField] public int killWaveBoss = 55;

    UIManager uIManager;
    PlayerController playerController;
    public GameStatus gameStatus;
    public GameResult gameResult;
    public Waves wave;
    public int killCount = 0;
    bool onetime1 = false;
    bool onetime2 = false;
    bool onetime3 = false;

    private void Start()
    {
        //ottengo l' UI manager
        uIManager = FindObjectOfType<UIManager>();
        //ottengo il player
        playerController = FindObjectOfType<PlayerController>();

        //settiamo le variabili iniziali
        gameStatus = GameStatus.MenuStart;
        wave = Waves.Wave1;
        uIManager.WaveNumber("Wave 1");
    }

    private void Update()
    {
        //aggiorno la ui del killcounter
        uIManager.KillCounterUI(killCount);


        //aggiornoi la ui
        if (wave == Waves.Wave1) 
        {
            uIManager.KillUntilNextWave_txt.text = "Kill next wave: " + (killWave2 - killCount);
        }else if (wave == Waves.Wave2)
        {
            uIManager.KillUntilNextWave_txt.text = "Kill next wave: " + (killWave3 - killCount);
        }
        else if (wave == Waves.Wave3)
        {
            uIManager.KillUntilNextWave_txt.text = "Kill until boss: " + (killWaveBoss - killCount);
        }
        else if (wave == Waves.Boss)
        {
            uIManager.KillUntilNextWave_txt.text = "";
        }

        //codice per mettere in pausa
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

        //codice per modificare i gamestatus
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


        //controllo e aggiorno le wave
        if (killCount == killWave2)
        {
            uIManager.WaveNumber("Wave 2");
            wave = Waves.Wave2;
            if (!onetime1)
            {
                playerController.healthPlayer += 50;
                onetime1 = true;
            }
        }
        else if (killCount == killWave3) 
        {
            uIManager.WaveNumber("Wave 3");
            wave = Waves.Wave3;
            if (!onetime2)
            {
                playerController.healthPlayer += 50;
                onetime2 = true;
            }
        }
        else if (killCount == killWaveBoss)
        {
            uIManager.WaveNumber("BOSS");
            wave = Waves.Boss;
            if (!onetime3)
            {
                playerController.healthPlayer += 50;
                onetime3 = true;
            }
        }

        //Aggiorno ui
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

    //esci dal gioco
    public void QuitGame()
    {
        Application.Quit();
    }

    //ricarica la scena
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    


}
