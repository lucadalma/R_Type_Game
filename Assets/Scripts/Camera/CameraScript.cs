using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
    }

}
