using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    [SerializeField] private float returnTime = 3f;
    [SerializeField] private float invinsibilityTime = 5f;
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private bool isPlayerDead = false;
    [SerializeField] public bool isPlayerInvinsible = false;
    [SerializeField] public bool isPlayerUsingShield = false;

    private Color shieldColour;
    private float originalAlpha;
    private float originalInvinsibilityTime;
    private GameObject shield;
    private GameObject spaceship;

    private void Awake()
    {
        shieldColour = shieldMaterial.color;
        originalAlpha = 0.5f;
        originalInvinsibilityTime = 5f;

        shield = transform.GetChild(0).GetChild(2).gameObject;
        spaceship = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        // If player is dead, return the player to the game after 3s
        if (isPlayerDead)
        {
            returnTime = returnTime - Time.deltaTime;

            if (returnTime <= 0)
            {
                isPlayerInvinsible = true;
                isPlayerDead = false;
                returnTime = 3f;

                transform.GetChild(0).gameObject.SetActive(true);
                gameObject.GetComponent<PlayerMovement>().enabled = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if(!isPlayerInvinsible && !isPlayerUsingShield && other.tag.Contains(Properties.ENEMY_TAG))
        {
            isPlayerDead = true;

            spaceship.GetComponent<PowerUp>().currentPowerUp = PowerUp.powerUpEnum.singleBullet;
            spaceship.GetComponent<PowerUpShield>().shieldTimer = 30f;
            spaceship.GetComponent<PowerUp>().isShieldActivated = false;

            spaceship.gameObject.SetActive(false);
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
