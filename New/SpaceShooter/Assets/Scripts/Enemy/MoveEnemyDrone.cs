using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyDrone : MonoBehaviour
{
    [SerializeField] private float circleRadius = 750f;
    [SerializeField] private float rotationSpeed = 0.25f;
    [SerializeField] private float trackTransitionSpeed = 0.5f;
    [SerializeField] private float fireTime = 2f;
    [SerializeField] private float enemyBulletSpeed = 500f;
    [SerializeField] private GameObject enemyDrone;
    [SerializeField] private Rigidbody enemyBullet;
    [SerializeField] private Transform droneGun;

    private Transform playerTransform;

    private Vector3 positionOffset;
    private Vector3 positionDifference;
    private float circleAngle;
    private float angleBetweenDroneAndPlayer;
    private void Awake()
    {
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
    }

    void FixedUpdate()
    {
        fireTime = fireTime - Time.deltaTime;

        if(fireTime <= 0f)
        {
            fireTime = 2f;
            Fire();
        }

        Move();
    }

    private void Move()
    {
        // Making the drone circle around the player
        positionOffset.Set(Mathf.Cos(circleAngle) * circleRadius,
                           Mathf.Sin(circleAngle) * circleRadius,
                           Properties.PLAYER_Z_POSITION);

        var desiredPosition = playerTransform.position + positionOffset;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * trackTransitionSpeed);

        circleAngle = circleAngle + Time.deltaTime * rotationSpeed;

        // Making the drone face the player
        positionDifference = playerTransform.position - transform.position;
        angleBetweenDroneAndPlayer = 270 + Mathf.Atan2(positionDifference.y, positionDifference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBetweenDroneAndPlayer));
    }

    private void Fire()
    {
        Rigidbody bulletCenterRigidBody = Instantiate(enemyBullet, droneGun.position, droneGun.rotation);
        bulletCenterRigidBody.AddForce(transform.up * enemyBulletSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == Properties.SPACESHIP_NAME)
        {
            Destroy(gameObject);
        }

        if(other.name == Properties.PLAYER_BULLET || other.name == Properties.PLAYER_LASER)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
