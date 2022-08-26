using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BossMovement : MonoBehaviour
{
    private GameObject screenBoundaryTopLeft;
    private Transform playerTransform;
    private Vector3 positionDifference;
    private float angleBetweenDroneAndPlayer;
    private float offset = 100f;

    // Start is called before the first frame update
    void Awake()
    {
        screenBoundaryTopLeft = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_LEFT_INNER);
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 desiredPosition = new Vector3(playerTransform.position.x,
                                               screenBoundaryTopLeft.transform.position.y - offset,
                                               Properties.PLAYER_Z_POSITION);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);

        // Making the boss face the player
        //positionDifference = playerTransform.position - transform.position;
        //angleBetweenDroneAndPlayer = 90 + Mathf.Atan2(positionDifference.y, positionDifference.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBetweenDroneAndPlayer));
    }
}
