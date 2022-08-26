using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShield : MonoBehaviour
{
    [SerializeField] public float shieldTimer = 30f;
    [SerializeField] private Material shieldMaterial;

    private Color shieldColour;
    private float originalAlpha;
    private float originalShieldTimer;
    private PowerUp powerUp;
    private GameObject shield;

    private void Awake()
    {
        shieldColour = shieldMaterial.color;
        originalAlpha = 0.5f;
        originalShieldTimer = 30f;

        powerUp = GetComponent<PowerUp>();
        shield = GameObject.Find(Properties.PLAYER_SHIELD);
        shield.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(powerUp.isShieldActivated)
        {
            gameObject.GetComponentInParent<DestroyPlayer>().isPlayerUsingShield = true;
            shield.SetActive(true);

            shieldTimer = shieldTimer - Time.deltaTime;

            shieldColour.a = Mathf.Max(0.075f, originalAlpha * (shieldTimer / originalShieldTimer));
            shieldMaterial.color = shieldColour;

            if(shieldTimer <= 0)
            {
                gameObject.GetComponentInParent<DestroyPlayer>().isPlayerUsingShield = false;

                shield.SetActive(false);
                powerUp.isShieldActivated = false;
                shieldTimer = 30f;

                shieldColour.a = originalAlpha;
                shieldMaterial.color = shieldColour;
            }

            // If the player gets another shield while the first shield is still active, then increase the shield timer to 30 and deactivate the second shield
            if (powerUp.isSecondShieldActivated)
            {
                shieldTimer = 30f;
                powerUp.isSecondShieldActivated = false;
            }
        }
    }
}
