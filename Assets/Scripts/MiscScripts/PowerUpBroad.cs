using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBroad : MonoBehaviour
{
    public float launchForce = 700f;
    public bool useBroad = false;
    public Rigidbody broadRigidbody;
    private Target target;
    public float timer = 0f;
    float waitingTime = 40f;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Target>();
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
        gameObject.transform.position = new Vector3(0f, 0f, 20f);
    }
    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.name == "Intergalactic Spaceship")
        {
            useBroad = true;
            target.hitPoint = 3;
            gameObject.SetActive(false);
            //waitingTime = Random.Range(40f, 60f);
            waitingTime = 10f;
        }
    }

    public void ShootBroad(Transform[] firePoints)
    {
        gameObject.SetActive(true);
        foreach(var firePoint in firePoints)
        {
            if(firePoint.name == "Fire Point Front")
            {
                var projectileInstanceRightSpread = Instantiate
                (
                    broadRigidbody,
                    firePoint.position,
                    firePoint.rotation
                );
                projectileInstanceRightSpread.velocity = transform.TransformDirection(0f, 0f, 100f);
            }
        }
    }
}
