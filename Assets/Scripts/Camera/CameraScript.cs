using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 5f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();    
    }

    void Update()
    {
        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);

        if (gameManager.wave == GameManager.Waves.Boss) 
        {
            cameraSpeed = 0;
        }
    }

}
