using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BossFire : MonoBehaviour
{
    [SerializeField] private Transform gunLeft;
    [SerializeField] private Transform gunRight;
    [SerializeField] private Transform gunCenterLeft;
    [SerializeField] private Transform gunCenterRight;
    [SerializeField] private Rigidbody stunBullet;
    [SerializeField] private Rigidbody enemyBullet;
    [SerializeField] private Rigidbody enemyLaser;
    [SerializeField] private AudioClip enemyStunBulletAudioClip;
    [SerializeField] private AudioClip enemyBulletAudioClip;
    [SerializeField] private AudioClip enemyLaserAudioClip;
    [SerializeField] private float enemyProjectileSpeed = 2000f;
    [SerializeField] private float stunBulletTime = 1f;
    [SerializeField] private float bulletTime = 1f;
    [SerializeField] private float laserTime = 2f;
    [SerializeField] private GameObject[] lightningParticles;
    [SerializeField] private float lightningToggleTime = 2f;
    [SerializeField] private bool shouldToggleLightningParticlesOff;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        shouldToggleLightningParticlesOff = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!DestroyLevel1Boss.isBossDead)
        {
            stunBulletTime = stunBulletTime - Time.deltaTime;

            if (stunBulletTime <= 0)
            {
                stunBulletTime = 15f;
                FireStunBullet();

                ToggleLightningParticles(true);
                shouldToggleLightningParticlesOff = true;
            }

            if(shouldToggleLightningParticlesOff)
            {
                lightningToggleTime = lightningToggleTime - Time.deltaTime;

                if (lightningToggleTime <= 0)
                {
                    lightningToggleTime = 2f;   // Lightning particle effect has a duration of 2s
                    ToggleLightningParticles(false);
                    shouldToggleLightningParticlesOff = false;
                }
            }

            bulletTime = bulletTime - Time.deltaTime;

            if(bulletTime <= 0)
            {
                bulletTime = 1f;
                FireBullets();
            }

            laserTime = laserTime - Time.deltaTime;

            if (laserTime <= 0)
            {
                laserTime = 2f;
                FireLaser();
            }
        }
    }

    private void ToggleLightningParticles(bool value)
    {
        PlayerMovement.sphereCollider.enabled = value;  // enabling/disabling the sphere collider of the player based on whether the boss is about to fire the stun bullet

        foreach (GameObject lightningParticle in lightningParticles)
            lightningParticle.SetActive(value);
    }

    private void FireStunBullet()
    {
        audioSource.PlayOneShot(enemyStunBulletAudioClip);
    }

    private void FireBullets()
    {
        // left gun
        Rigidbody bulletLeft_center = Instantiate(enemyBullet, gunLeft.position, gunLeft.rotation);
        bulletLeft_center.AddForce(-transform.up * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletLeft_left = Instantiate(enemyBullet, gunLeft.position, gunLeft.rotation);
        bulletLeft_left.AddForce((-transform.up - transform.right / 2) * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletLeft_left90 = Instantiate(enemyBullet, gunLeft.position, gunLeft.rotation);
        bulletLeft_left90.AddForce(transform.right * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletLeft_right = Instantiate(enemyBullet, gunLeft.position, gunLeft.rotation);
        bulletLeft_right.AddForce((-transform.up + transform.right / 2) * enemyProjectileSpeed, ForceMode.Impulse);

        // right gun
        Rigidbody bulletRight_center = Instantiate(enemyBullet, gunRight.position, gunRight.rotation);
        bulletRight_center.AddForce(-transform.up * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletRight_left = Instantiate(enemyBullet, gunRight.position, gunRight.rotation);
        bulletRight_left.AddForce((-transform.up - transform.right / 2) * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletRight_right = Instantiate(enemyBullet, gunRight.position, gunRight.rotation);
        bulletRight_right.AddForce((-transform.up + transform.right / 2) * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletRight_right90 = Instantiate(enemyBullet, gunRight.position, gunRight.rotation);
        bulletRight_right90.AddForce(-transform.right * enemyProjectileSpeed, ForceMode.Impulse);

        // center-left gun
        Rigidbody bulletCenterLeft_center = Instantiate(enemyBullet, gunCenterLeft.position, gunCenterLeft.rotation);
        bulletCenterLeft_center.AddForce(-transform.up * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletCenterLeft_left = Instantiate(enemyBullet, gunCenterLeft.position, gunCenterLeft.rotation);
        bulletCenterLeft_left.AddForce((-transform.up - transform.right / 2) * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletCenterLeft_right = Instantiate(enemyBullet, gunCenterLeft.position, gunCenterLeft.rotation);
        bulletCenterLeft_right.AddForce((-transform.up + transform.right / 2) * enemyProjectileSpeed, ForceMode.Impulse);

        // center-right gun
        Rigidbody bulletCenterRight_center = Instantiate(enemyBullet, gunCenterRight.position, gunCenterRight.rotation);
        bulletCenterRight_center.AddForce(-transform.up * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletCenterRight_left = Instantiate(enemyBullet, gunCenterRight.position, gunCenterRight.rotation);
        bulletCenterRight_left.AddForce((-transform.up - transform.right / 2) * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody bulletCenterRight_right = Instantiate(enemyBullet, gunCenterRight.position, gunCenterRight.rotation);
        bulletCenterRight_right.AddForce((-transform.up + transform.right / 2) * enemyProjectileSpeed, ForceMode.Impulse);

        audioSource.PlayOneShot(enemyBulletAudioClip);
    }

    private void FireLaser()
    {
        Rigidbody laserLeft = Instantiate(enemyLaser, gunLeft.position, gunLeft.rotation);
        laserLeft.AddForce(-transform.up * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody laserRight = Instantiate(enemyLaser, gunRight.position, gunRight.rotation);
        laserRight.AddForce(-transform.up * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody laserCenterLeft = Instantiate(enemyLaser, gunCenterLeft.position, gunLeft.rotation);
        laserCenterLeft.AddForce(-transform.up * enemyProjectileSpeed, ForceMode.Impulse);

        Rigidbody laserCenterRight = Instantiate(enemyLaser, gunCenterRight.position, gunRight.rotation);
        laserCenterRight.AddForce(-transform.up * enemyProjectileSpeed, ForceMode.Impulse);

        audioSource.PlayOneShot(enemyLaserAudioClip);
    }
}
