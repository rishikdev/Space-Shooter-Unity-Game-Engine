using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Transform centerGunTransform;
    [SerializeField] private Transform leftGunTransform;
    [SerializeField] private Transform rightGunTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private Rigidbody playerBullet;
    [SerializeField] private Rigidbody playerLaser;
    [SerializeField] private AudioClip singleBulletAudioClip;
    [SerializeField] private AudioClip tripleBulletsAudioClip;
    [SerializeField] private AudioClip spreadAudioClip;
    [SerializeField] private AudioClip laserAudioClip;
    [SerializeField] private float playerBulletSpeed = 2500f;
    [SerializeField] private float playerBulletInterval = 0.15f;

    private PlayerInputActions playerControls;
    private InputAction playerFire;
    private InputAction turboFire;
    private PowerUp powerUp;
    private AudioSource audioSource;
    private bool isFireButtonPressed;

    private void Awake()
    {
        playerControls = new PlayerInputActions();

        powerUp = GetComponent<PowerUp>();
        audioSource = GetComponent<AudioSource>();
        isFireButtonPressed = false;
    }

    private void OnEnable()
    {
        playerFire = playerControls.Player.Fire;
        playerFire.Enable();

        turboFire = playerControls.Player.TurboFire;
        turboFire.Enable();

        // Registering Fire() to event
        playerFire.performed += onFire;

        // Creating a turbo button
        turboFire.started += CreateTurboButton;

        // Removing the turbo button
        turboFire.canceled += RemoveTurboButton;
    }

    private void OnDisable()
    {
        playerFire.Disable();
        turboFire.Disable();
    }

    private void Update()
    {
        playerBulletInterval = playerBulletInterval - Time.deltaTime;

        if (playerBulletInterval <= 0 && isFireButtonPressed)
        {
            playerBulletInterval = 0.15f;
            Fire();
        }
    }

    private void onFire(InputAction.CallbackContext callbackContext)
    {
        Fire();
    }

    private void CreateTurboButton(InputAction.CallbackContext callbackContext)
    {
        isFireButtonPressed = true;
    }

    private void RemoveTurboButton(InputAction.CallbackContext callbackContext)
    {
        isFireButtonPressed = false;
    }

    private void Fire()
    {
        playerBulletSpeed = 2500f;

        switch (powerUp.currentPowerUp)
        {
            case PowerUp.powerUpEnum.singleBullet:
                SingleBulletPowerUp();
                audioSource.PlayOneShot(singleBulletAudioClip);
                break;

            case PowerUp.powerUpEnum.tripleBullets:
                TripleBulletsPowerUp();
                audioSource.PlayOneShot(tripleBulletsAudioClip);
                break;

            case PowerUp.powerUpEnum.spread:
                SpreadPowerUp();
                audioSource.PlayOneShot(spreadAudioClip);
                break;

            case PowerUp.powerUpEnum.laser:
                LaserPowerUp();
                audioSource.PlayOneShot(laserAudioClip);
                break;

            default:
                SingleBulletPowerUp();
                audioSource.PlayOneShot(singleBulletAudioClip);
                break;
        }
    }

    private void SingleBulletPowerUp()
    {
        Rigidbody bulletCenterRigidBody = Instantiate(playerBullet, centerGunTransform.position, centerGunTransform.rotation);
        bulletCenterRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
    }

    private void TripleBulletsPowerUp()
    {
        Rigidbody bulletCenterRigidBody = Instantiate(playerBullet, centerGunTransform.position, centerGunTransform.rotation);
        Rigidbody bulletLeftRigidBody = Instantiate(playerBullet, leftGunTransform.position, leftGunTransform.rotation);
        Rigidbody bulletRightRigidBody = Instantiate(playerBullet, rightGunTransform.position, rightGunTransform.rotation);

        bulletCenterRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
        bulletLeftRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
        bulletRightRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
    }

    private void SpreadPowerUp()
    {
        Rigidbody bulletCenterRigidBody = Instantiate(playerBullet, centerGunTransform.position, centerGunTransform.rotation);
        Rigidbody bulletCenter_LeftRigidBody = Instantiate(playerBullet, centerGunTransform.position, centerGunTransform.rotation);
        Rigidbody bulletCenter_RightRigidBody = Instantiate(playerBullet, centerGunTransform.position, centerGunTransform.rotation);

        Rigidbody bulletLeftRigidBody = Instantiate(playerBullet, leftGunTransform.position,leftGunTransform.rotation);
        Rigidbody bulletLeft_LeftRigidBody = Instantiate(playerBullet, leftGunTransform.position, leftGunTransform.rotation);
        Rigidbody bulletLeft_RightRigidBody = Instantiate(playerBullet, leftGunTransform.position, leftGunTransform.rotation);

        Rigidbody bulletRightRigidBody = Instantiate(playerBullet, rightGunTransform.position, rightGunTransform.rotation);
        Rigidbody bulletRight_LeftRigidBody = Instantiate(playerBullet, rightGunTransform.position, rightGunTransform.rotation);
        Rigidbody bulletRight_RightRigidBody = Instantiate(playerBullet, rightGunTransform.position, rightGunTransform.rotation);

        bulletCenterRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
        bulletCenter_LeftRigidBody.AddForce(-(transform.up + transform.right / 2) * playerBulletSpeed, ForceMode.Impulse);
        bulletCenter_RightRigidBody.AddForce(-(transform.up - transform.right / 2) * playerBulletSpeed, ForceMode.Impulse);

        bulletLeftRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
        bulletLeft_LeftRigidBody.AddForce(-(transform.up + transform.right / 2) * playerBulletSpeed, ForceMode.Impulse);
        bulletLeft_RightRigidBody.AddForce(-(transform.up - transform.right / 2) * playerBulletSpeed, ForceMode.Impulse);

        bulletRightRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
        bulletRight_LeftRigidBody.AddForce(-(transform.up + transform.right / 2) * playerBulletSpeed, ForceMode.Impulse);
        bulletRight_RightRigidBody.AddForce(-(transform.up - transform.right / 2) * playerBulletSpeed, ForceMode.Impulse);
    }

    private void LaserPowerUp()
    {
        playerBulletSpeed = 5000f;

        Rigidbody laserLeftRigidBody = Instantiate(playerLaser, leftGunTransform.position ,leftGunTransform.rotation);
        Rigidbody laserRightRigidBody = Instantiate(playerLaser, rightGunTransform.position, rightGunTransform.rotation);

        laserLeftRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
        laserRightRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
    }
}
