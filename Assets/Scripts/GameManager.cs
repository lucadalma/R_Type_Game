using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] public GameObject enemyLevel;

    public enum GameStatus 
    {
       MenuStart,
       GameRunning,
       GamePause,
       GameEnd
    }
}
