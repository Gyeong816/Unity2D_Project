using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffect = 0.5f; 
    private Vector3 lastCameraPos;


    void Start()
    {
        lastCameraPos = cameraTransform.position;
    }

    void Update()
    {
   
        Vector3 deltaMovement = cameraTransform.position - lastCameraPos;
        transform.position += deltaMovement * parallaxEffect;
        

        lastCameraPos = cameraTransform.position;
    }

}
