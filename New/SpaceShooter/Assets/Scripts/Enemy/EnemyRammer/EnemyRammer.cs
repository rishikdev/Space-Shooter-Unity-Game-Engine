using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRammer : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2f;

    private Transform playerTransform;
    private GameObject explosionParticle;
    private Vector3 positionDifference;
    private float angleBetweenDroneAndPlayer;
    private AudioSource audioSource;
    private UI ui;
    private PlayEnemyDestructionSound playEnemyDestructionSound;
    private Enemy enemy;

    void Awake()
    {
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
        audioSource = GetComponent<AudioSource>();
        ui = GameObject.Find(Properties.UI_CANVAS).GetComponent<UI>();

        playEnemyDestructionSound = GameObject.Find(Properties.ENEMY_DESTRUCTION_AUDIO_SOURCE).GetComponent<PlayEnemyDestructionSound>();
        explosionParticle = transform.Find(Properties.EXPLOSION_PARTICLE).gameObject;
        explosionParticle.SetActive(false);

        enemy = GameObject.Find(Properties.ENEMY).GetComponent<Enemy>();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 desiredPosition = playerTransform.position;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * enemySpeed);

        // Making the drone face the player
        positionDifference = playerTransform.position - transform.position;
        angleBetweenDroneAndPlayer = 270 + Mathf.Atan2(positionDifference.y, positionDifference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBetweenDroneAndPlayer));
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
        if (other.name == Properties.PLAYER_BULLET || other.name == Properties.PLAYER_LASER)
        {
            DestroyEnemy();
            Destroy(other.gameObject);
        }
    }

    private void DestroyEnemy()
    {
        enemy.SetEnemyRammerKilledCount();

        audioSource.volume = 0.9f;
        explosionParticle.SetActive(true);
        playEnemyDestructionSound.PlaySmallSpaceshipDestructionSound();

        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        foreach (Transform child in transform)
            if (child.name != Properties.EXPLOSION_PARTICLE)
                child.gameObject.SetActive(false);

        Destroy(gameObject, 2);

        ui.UpdateCurrentScore(Properties.ENEMY_RAMMER_HIT_POINT);
    }
}
