using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpread : MonoBehaviour
{
    public float launchForce = 700f;
    public bool useSpread = false;
    public Rigidbody bulletRigidbody;
    public float timer = 0f;
    float waitingTime = 30f;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;
        if(timer > waitingTime)
        {
            gameObject.SetActive(true);
            timer = 0f;
            CallIntoScene();
        }
    }

    void CallIntoScene()
    {
        gameObject.transform.position = new Vector3(0f, 0f, 30f);
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.name == "Intergalactic Spaceship")
        {
            useSpread = true;
            gameObject.SetActive(false);
            //waitingTime = Random.Range(30f, 50f);
            waitingTime = 10f;
        }
    }

    public void ShootSpread(Transform[] firePoints)
    {
        gameObject.SetActive(true);
        foreach(var firePoint in firePoints)
        {
            if(firePoint.name == "Fire Point Right Spread")
            {
                var projectileInstanceRightSpread = Instantiate
                (
                    bulletRigidbody,
                    firePoint.position,
                    firePoint.rotation
                );
                projectileInstanceRightSpread.velocity = transform.TransformDirection(25f, 0, 100f);
            }

            if(firePoint.name == "Fire Point Left Spread")
            {
                var projectileInstanceLeftSpread = Instantiate
                (
                    bulletRigidbody,
                    firePoint.position,
                    firePoint.rotation
                );

                projectileInstanceLeftSpread.velocity = transform.TransformDirection(-25f, 0, 100f);
            }
        }
    }
}
