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

    [SerializeField] private float playerBulletSpeed = 1000f;

    private PlayerInputActions playerControls;
    private InputAction playerFire;
    private PowerUp powerUp;

    private void Awake()
    {
        playerControls = new PlayerInputActions();

        powerUp = GetComponent<PowerUp>();
    }

    private void OnEnable()
    {
        playerFire = playerControls.Player.Fire;
        playerFire.Enable();

        // Registering Fire() to event
        playerFire.performed += onFire;
    }

    private void OnDisable()
    {
        playerFire.Disable();
    }

    private void onFire(InputAction.CallbackContext callbackContext)
    {
        switch(powerUp.currentPowerUp)
        {
            case PowerUp.powerUpEnum.singleBullet:
                SingleBulletPowerUp();
                break;

            case PowerUp.powerUpEnum.tripleBullets:
                TripleBulletsPowerUp();
                break;

            case PowerUp.powerUpEnum.spread:
                SpreadPowerUp();
                break;

            case PowerUp.powerUpEnum.laser:
                LaserPowerUp();
                break;

            default:
                SingleBulletPowerUp();
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
        Rigidbody laserLeftRigidBody = Instantiate(playerLaser, leftGunTransform.position ,leftGunTransform.rotation);
        Rigidbody laserRightRigidBody = Instantiate(playerLaser, rightGunTransform.position, rightGunTransform.rotation);

        laserLeftRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
        laserRightRigidBody.AddForce(-transform.up * playerBulletSpeed, ForceMode.Impulse);
    }
}
