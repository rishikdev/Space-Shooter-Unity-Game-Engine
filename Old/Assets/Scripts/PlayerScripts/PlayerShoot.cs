using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private Transform[] firePoints;
    [SerializeField]
    private Rigidbody bulletRigidbody;
    [SerializeField]
    private float launchForce = 700f;
    public GameObject cameraTracker;
    private KeepButtonPressed fireButton;
    public float timer = 0f;
    public float waitingTime = 0.4f;
    private Bullet bullet;
    private PowerUpSpread powerUpSpread;
    private PowerUpBroad powerUpBroad;
    // Start is called before the first frame update
    void Start()
    {
        fireButton = FindObjectOfType<KeepButtonPressed>();
        powerUpSpread = FindObjectOfType<PowerUpSpread>();
        powerUpBroad = FindObjectOfType<PowerUpBroad>();
    }

    // Update is called once per frame
    public void Update()
    {
        timer = timer + Time.deltaTime;
        if(fireButton.pressed && timer > waitingTime)
        {
            timer = 0f;
            Shoot();

            if(powerUpSpread.useSpread)
                powerUpSpread.ShootSpread(firePoints);
            
            if(powerUpBroad.useBroad)
                powerUpBroad.ShootBroad(firePoints);
        }
        if(!fireButton.pressed)
            fireButton.pressed = false;
    }

    public void Shoot()
    {

        foreach(var firePoint in firePoints)
        {
            if(firePoint.name == "Fire Point Left" || firePoint.name == "Fire Point Right")
            {
                var projectileInstance = Instantiate
                (
                    bulletRigidbody,
                    firePoint.position,
                    firePoint.rotation
                );

                projectileInstance.velocity = transform.TransformDirection(0, 0, 100f);
                projectileInstance.AddForce(firePoint.forward.normalized * launchForce);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
