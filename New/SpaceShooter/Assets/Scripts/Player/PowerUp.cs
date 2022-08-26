using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private AudioClip powerUpAudioClip;
    public enum powerUpEnum
    {
        singleBullet, tripleBullets, spread, laser,
    }

    public bool isShieldActivated;
    public bool isSecondShieldActivated;    // This variable is used when the player gets another shield while the first shield is still active
    public powerUpEnum currentPowerUp;

    private UI score;
    private AudioSource audioSource;

    void Awake()
    {
        currentPowerUp = powerUpEnum.singleBullet;
        score = GameObject.Find(Properties.UI_CANVAS).GetComponent<UI>();

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == Properties.POWERUP_SINGLEBULLET)
        {
            currentPowerUp = powerUpEnum.singleBullet;
            score.UpdateCurrentScore(Properties.POWERUP_POINT);
            audioSource.PlayOneShot(powerUpAudioClip);
            Destroy(other.gameObject);
        }

        else if (other.name == Properties.POWERUP_TRIPLEBULLETS)
        {
            currentPowerUp = powerUpEnum.tripleBullets;
            score.UpdateCurrentScore(Properties.POWERUP_POINT);
            audioSource.PlayOneShot(powerUpAudioClip);
            Destroy(other.gameObject);
        }

        else if (other.name == Properties.POWERUP_SPREAD)
        {
            currentPowerUp = powerUpEnum.spread;
            score.UpdateCurrentScore(Properties.POWERUP_POINT);
            audioSource.PlayOneShot(powerUpAudioClip);
            Destroy(other.gameObject);
        }

        else if (other.name == Properties.POWERUP_LASER)
        {
            currentPowerUp = powerUpEnum.laser;
            score.UpdateCurrentScore(Properties.POWERUP_POINT);
            audioSource.PlayOneShot(powerUpAudioClip);
            Destroy(other.gameObject);
        }

        else if (other.name == Properties.POWERUP_SHIELD)
        {
            score.UpdateCurrentScore(Properties.POWERUP_POINT);

            if (isShieldActivated)
                isSecondShieldActivated = true;

            isShieldActivated = true;
            audioSource.PlayOneShot(powerUpAudioClip);
            Destroy(other.gameObject);
        }
    }
}
