using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject StartGameCanvas;
    [SerializeField] GameObject PauseGameCanvas;
    [SerializeField] GameObject EndGameCanvas;
    [SerializeField] GameObject InMenuCanvas;
    [SerializeField] GameObject Win_txt;
    [SerializeField] GameObject Lose_txt;
    [SerializeField] Text Wave_txt;
    [SerializeField] Text KillCounter_txt;


    public void StartGame() 
    {
        StartGameCanvas.SetActive(true);
        PauseGameCanvas.SetActive(false);
        EndGameCanvas.SetActive(false);
        InMenuCanvas.SetActive(false);

    }

    public void PauseGame()
    {
        StartGameCanvas.SetActive(false);
        PauseGameCanvas.SetActive(true);
        EndGameCanvas.SetActive(false);
        InMenuCanvas.SetActive(false);

    }

    public void EndGame()
    {
        StartGameCanvas.SetActive(false);
        PauseGameCanvas.SetActive(false);
        EndGameCanvas.SetActive(true);
        InMenuCanvas.SetActive(false);

    }

    public void GameRunning()
    {
        StartGameCanvas.SetActive(false);
        PauseGameCanvas.SetActive(false);
        EndGameCanvas.SetActive(false);
        InMenuCanvas.SetActive(true);
    }

    public void PlayerWinUI()
    {
        Win_txt.SetActive(true);
        Lose_txt.SetActive(false);
    }

    public void PlayerLoseUI()
    {
        Win_txt.SetActive(false);
        Lose_txt.SetActive(true);
    }

    public void WaveNumber(string waveNumber) 
    {
        Wave_txt.text = waveNumber;
    }

    public void KillCounterUI(int number)
    {
        KillCounter_txt.text = "Kill: " + number.ToString();
    }


}
