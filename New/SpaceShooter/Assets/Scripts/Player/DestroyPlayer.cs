using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlayer : MonoBehaviour
{
    [SerializeField] private float playerHealth = 100;
    [SerializeField] private float returnTime = 3f;
    [SerializeField] private float invinsibilityTime = 5f;
    [SerializeField] private float playerStunnedTime = 10f;
    [SerializeField] public int rest = 3;  // Remaining lives
    [SerializeField] private bool isPlayerDead = false;
    [SerializeField] public bool isPlayerInvinsible = false;
    [SerializeField] public bool isPlayerUsingShield = false;
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private AudioClip playerExplosionAudioClip;
    [SerializeField] private AudioClip bulletImpactAudioClip;
    [SerializeField] private AudioClip bulletShieldImpactAudioClip;
    [SerializeField] private AudioClip stunBulletImpactAudioClip;
    [SerializeField] private AudioClip stunnedAudioClip;

    private Color shieldColour;
    private float originalAlpha;
    private float originalInvinsibilityTime;
    private GameObject shield;
    private GameObject spaceship;
    private GameObject explosionParticle;
    private UI ui;
    private Transition transition;
    private AudioSource audioSource;

    private void Awake()
    {
        playerHealth = Properties.PLAYER_MAX_HEALTH;

        shieldColour = shieldMaterial.color;
        originalAlpha = 0.5f;
        originalInvinsibilityTime = 5f;
        playerStunnedTime = 10f;
        rest = 3;

        shield = transform.GetChild(0).GetChild(2).gameObject;
        spaceship = transform.GetChild(0).gameObject;

        ui = GameObject.Find(Properties.UI_CANVAS).GetComponent<UI>();
        transition = GameObject.Find(Properties.TRANSITION).GetComponent<Transition>();

        audioSource = GetComponent<AudioSource>();

        explosionParticle = transform.Find(Properties.EXPLOSION_PARTICLE).gameObject;
        explosionParticle.SetActive(false);
    }

    private void Update()
    {
        if (rest > 0)
        {
            if(PlayerMovement.isPlayerStunned)
            {
                playerStunnedTime = playerStunnedTime - Time.deltaTime;

                if(playerStunnedTime <= 0)
                {
                    playerStunnedTime = 10f;
                    PlayerMovement.isPlayerStunned = false;
                    ui.ChangePlayerHealthBarColour(false);
                }
            }

            // If player is dead, return the player to the game after 3s
            if (isPlayerDead)
            {
                returnTime = returnTime - Time.deltaTime;
                PlayerMovement.sphereCollider.enabled = false;

                if (returnTime <= 0)
                {
                    PlayerMovement.isPlayerStunned = false;
                    playerStunnedTime = 10f;

                    playerHealth = Properties.PLAYER_MAX_HEALTH;
                    ui.UpdateHealth(playerHealth);
                    ui.ChangePlayerHealthBarColour(false);

                    explosionParticle.SetActive(false);

                    isPlayerInvinsible = true;
                    isPlayerDead = false;
                    isPlayerUsingShield = false;
                    returnTime = 3f;
               
                    transform.GetChild(0).gameObject.SetActive(true);
                    gameObject.GetComponent<PlayerMovement>().enabled = true;

                    ui.Text_Ugh_Diddums.enabled = false;
                }
            }

            // After the player respawns, make the player invinsible for 5s
            if (isPlayerInvinsible)
            {
                invinsibilityTime = invinsibilityTime - Time.deltaTime;
                shield.SetActive(true);

                shieldColour.a = originalAlpha * (invinsibilityTime / originalInvinsibilityTime);
                shieldMaterial.color = shieldColour;

                if (invinsibilityTime <= 0)
                {
                    invinsibilityTime = 5f;
                    shield.SetActive(false);

                    isPlayerInvinsible = false;

                    shieldColour.a = originalAlpha;
                    shieldMaterial.color = shieldColour;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isPlayerInvinsible && other.tag == Properties.ENEMY_BOSS_TAG)
        {
            KillPlayer();
        }

        if ((isPlayerInvinsible || isPlayerUsingShield) && (other.tag == Properties.ENEMY_TAG || other.tag == Properties.ENEMY_BULLET_TAG))
        {
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(bulletShieldImpactAudioClip);
        }

        if(!isPlayerInvinsible && !isPlayerUsingShield && (other.tag == Properties.ENEMY_TAG || other.tag == Properties.ENEMY_BULLET_TAG))
        {
            playerHealth = playerHealth - 10;

            if (playerHealth <= 0)
            {
                KillPlayer();
            }

            else
            {
                audioSource.volume = 0.1f;
                audioSource.PlayOneShot(bulletImpactAudioClip);
                
                ui.UpdateHealth(playerHealth);
            }
        }
    }

    private void KillPlayer()
    {
        isPlayerDead = true;
        playerHealth = 0;
        ui.ChangePlayerHealthBarColour(false);
        PlayerMovement.isPlayerStunned = false;
        audioSource.Stop();

        audioSource.volume = 1f;
        explosionParticle.SetActive(true);
        audioSource.PlayOneShot(playerExplosionAudioClip);

        spaceship.GetComponent<PowerUp>().currentPowerUp = PowerUp.powerUpEnum.singleBullet;
        spaceship.GetComponent<PowerUpShield>().shieldTimer = 30f;
        spaceship.GetComponent<PowerUp>().isShieldActivated = false;

        spaceship.gameObject.SetActive(false);
        gameObject.GetComponent<PlayerMovement>().enabled = false;

        rest = rest - 1;
        ui.UpdateRest(rest);

        if (rest == 0)
            transition.OnLevelOver();

        ui.Text_Ugh_Diddums.enabled = true;

        ui.UpdateHealth(playerHealth);
    }

    private void OnParticleCollision(GameObject other)
    {
        PlayerMovement.isPlayerStunned = true;
        PlayerMovement.sphereCollider.enabled = false;
        audioSource.PlayOneShot(stunBulletImpactAudioClip);
        audioSource.PlayOneShot(stunnedAudioClip);
        ui.ChangePlayerHealthBarColour(true);
    }
}
