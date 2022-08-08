using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    private Transform playerTransform;
    private Transform mainCameraTransform;

    void Awake()
    {
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
        mainCameraTransform = GameObject.Find(Properties.MAIN_CAMERA).transform;
    }

    private void FixedUpdate()
    {
        mainCameraTransform.position = playerTransform.position;
    }
}
