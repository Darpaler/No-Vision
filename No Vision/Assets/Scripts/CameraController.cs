using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables
    
    // An empty game object to hold the camera
    private GameObject cameraParent;

    // The GameManager
    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        cameraParent = new GameObject("CameraParent");
        cameraParent.transform.position = transform.position;
        transform.parent = cameraParent.transform;
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isControllingCamera)
        {
            cameraParent.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
            transform.Rotate(-Input.gyro.rotationRateUnbiased.x, 0, 0);
        }
    }
}
