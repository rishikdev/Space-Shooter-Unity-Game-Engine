using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlayer : MonoBehaviour
{
    [SerializeField] private float returnTime = 3f;
    [SerializeField] private float invinsibilityTime = 5f;
    [SerializeField] private Material shieldMaterial;
    [SerializeField] private bool isPlayerDead = false;
    [SerializeField] public bool isPlayerInvinsible = false;
    [SerializeField] public bool isPlayerUsingShield = false;
    [SerializeField] private int rest = 3;  // Remaining lives

    private Color shieldColour;
    private float originalAlpha;
    private float originalInvinsibilityTime;
    private GameObject shield;
    private GameObject spaceship;
    private UI ui;

    private void Awake()
    {
        shieldColour = shieldMaterial.color;
        originalAlpha = 0.5f;
        originalInvinsibilityTime = 5f;

        shield = transform.GetChild(0).GetChild(2).gameObject;
        spaceship = transform.GetChild(0).gameObject;

        ui = GameObject.Find(Properties.CANVAS).GetComponent<UI>();
    }

    private void Update()
    {
        if (rest > 0)
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

        else
        {
            // Currently, GameOver scene is at index 2
            SceneManager.LoadScene(2);
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

            rest = rest - 1;
            ui.UpdateRest(rest);

            ui.Text_Ugh_Diddums.enabled = true;
        }
    }
}
