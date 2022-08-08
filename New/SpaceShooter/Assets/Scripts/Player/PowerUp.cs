using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum powerUpEnum
    {
        singleBullet, tripleBullets, spread, laser,
    }

    public bool isShieldActivated;
    public bool isSecondShieldActivated;    // This variable is used when the player gets another shield while the first shield is still active

    public powerUpEnum currentPowerUp;

    //private PlayerFire playerFire;

    void Awake()
    {
        //playerFire = GetComponent<PlayerFire>();
        currentPowerUp = powerUpEnum.singleBullet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == Properties.POWERUP_SINGLEBULLET)
        {
            currentPowerUp = powerUpEnum.singleBullet;
            Destroy(other.gameObject);
        }

        else if (other.name == Properties.POWERUP_TRIPLEBULLETS)
        {
            currentPowerUp = powerUpEnum.tripleBullets;
            Destroy(other.gameObject);
        }

        else if (other.name == Properties.POWERUP_SPREAD)
        {
            currentPowerUp = powerUpEnum.spread;
            Destroy(other.gameObject);
        }

        else if (other.name == Properties.POWERUP_LASER)
        {
            currentPowerUp = powerUpEnum.laser;
            Destroy(other.gameObject);
        }

        else if (other.name == Properties.POWERUP_SHIELD)
        {
            if (isShieldActivated)
                isSecondShieldActivated = true;

            isShieldActivated = true;
            Destroy(other.gameObject);
        }
    }
}
