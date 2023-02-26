using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroller : MonoBehaviour
{

    [SerializeField] float tileSize;
    [SerializeField] [Range(0, 1)] float scrollSpeed;
    [SerializeField] float viewZone = 1;

    private Transform cameraTransform;
    private Transform[] tiles;
    private int leftIndex, rightIndex;
    private float lastCameraX;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        tiles = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            tiles[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = tiles.Length - 1;

    }

    void Update()
    {
        float deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * scrollSpeed);
        lastCameraX = cameraTransform.position.x;


        if (cameraTransform.position.x < (tiles[leftIndex].transform.position.x + viewZone)) 
        {
            //sinistra    
            SwitchLeft();
        }

        if (cameraTransform.position.x > (tiles[rightIndex].transform.position.x - viewZone)) 
        {
            //destra
            SwitchRight();
        }
    }

    private void SwitchLeft() 
    {
        tiles[rightIndex].position = Vector3.right * (tiles[leftIndex].position.x - tileSize);
        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0) 
        {
            rightIndex = tiles.Length - 1;
        }
    }
    private void SwitchRight()
    {
        tiles[leftIndex].position = Vector3.right * (tiles[rightIndex].position.x + tileSize);
        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == tiles.Length) 
        {
            leftIndex = 0;
        }
     }
}
