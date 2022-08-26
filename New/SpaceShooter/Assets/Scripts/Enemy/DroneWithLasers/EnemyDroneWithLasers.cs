using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDroneWithLasers : MonoBehaviour
{
    [SerializeField] private float droneHealth = 100;
    [SerializeField] private float circleRadius = 750f;
    [SerializeField] private float rotationSpeed = 0.25f;
    [SerializeField] private float trackTransitionSpeed = 0.5f;
    [SerializeField] private float fireTime = 2f;
    [SerializeField] private float enemyLaserSpeed = 2000f;
    [SerializeField] private GameObject enemyDrone;
    [SerializeField] private Rigidbody enemyLaser;
    [SerializeField] private Transform droneGunLeft;
    [SerializeField] private Transform droneGunRight;
    [SerializeField] private AudioClip bulletImpactAudioClip;
    [SerializeField] private AudioClip enemyLaserAudioClip;
    //[SerializeField] private AudioClip enemyExplosionAudioClip;

    private Transform playerTransform;

    private GameObject explosionParticle;

    private Vector3 positionOffset;
    private Vector3 positionDifference;

    private AudioSource audioSource;

    private float circleAngle;
    private float angleBetweenDroneAndPlayer;

    private bool isDead;

    private UI ui;
    private PlayEnemyDestructionSound playEnemyDestructionSound;
    private Enemy enemy;


    private void Awake()
    {
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
        audioSource = GetComponent<AudioSource>();
        ui = GameObject.Find(Properties.UI_CANVAS).GetComponent<UI>();
        isDead = false;
        playEnemyDestructionSound = GameObject.Find(Properties.ENEMY_DESTRUCTION_AUDIO_SOURCE).GetComponent<PlayEnemyDestructionSound>();

        explosionParticle = transform.Find(Properties.EXPLOSION_PARTICLE).gameObject;
        explosionParticle.SetActive(false);

        enemy = GameObject.Find(Properties.ENEMY).GetComponent<Enemy>();
    }

    void FixedUpdate()
    {
        fireTime = fireTime - Time.deltaTime;

        if(!isDead && fireTime <= 0f)
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
                           0);

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
        Rigidbody laserLeft = Instantiate(enemyLaser, droneGunLeft.position, droneGunLeft.rotation);
        laserLeft.AddForce(transform.up * enemyLaserSpeed, ForceMode.Impulse);

        Rigidbody laserRight = Instantiate(enemyLaser, droneGunRight.position, droneGunRight.rotation);
        laserRight.AddForce(transform.up * enemyLaserSpeed, ForceMode.Impulse);

        audioSource.PlayOneShot(enemyLaserAudioClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the enemy collided with the player, destroy the enemy. Player's destruction is handled elsewhere.
        if (other.name == Properties.SPACESHIP_NAME)
        {
            DestroyEnemy();
        }

        // If the player's bullet/laser hits the enemy, destroy the enemy as well as the player's bullet/laser
        // Also, increment the player's score
        else if(other.tag == Properties.PLAYER_BULLET_TAG)
        {
            DecreaseHealth(25, other);
        }

        else if(other.tag == Properties.PLAYER_LASER_TAG)
        {
            DecreaseHealth(50, other);
        }
    }

    private void DecreaseHealth(float amount, Collider other)
    {
        audioSource.volume = 0.1f;
        audioSource.PlayOneShot(bulletImpactAudioClip);
        droneHealth = droneHealth - amount;
        Destroy(other.gameObject);

        if (droneHealth <= 0)
        {
            DestroyEnemy();
            Destroy(other.gameObject);
        }
    }

    private void DestroyEnemy()
    {
        enemy.SetEnemyWithLasersKilledCount();

        isDead = true;
        
        audioSource.volume = 0.9f;
        explosionParticle.SetActive(true);
        playEnemyDestructionSound.PlaySmallSpaceshipDestructionSound();

        gameObject.GetComponent<SphereCollider>().enabled = false;

        foreach (Transform child in transform)
            if(child.name != Properties.EXPLOSION_PARTICLE)
            child.gameObject.SetActive(false);

        Destroy(gameObject, 2);

        ui.UpdateCurrentScore(Properties.ENEMY_DRONE_WITH_LASER_HIT_POINT);
    }
}
