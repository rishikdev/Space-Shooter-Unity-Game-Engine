using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCamera : MonoBehaviour
{
    private Transform playerTransform;
    private Transform mainCameraTransform;
    private Enemy enemy;

    void Awake()
    {
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
        mainCameraTransform = GameObject.Find(Properties.MAIN_CAMERA).transform;
        enemy = GameObject.Find(Properties.ENEMY).GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        if(!enemy.isBossActive)
            mainCameraTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
    }
}
