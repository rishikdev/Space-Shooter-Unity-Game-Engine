using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Rigidbody bulletRigidBody;
    [SerializeField]
    private float launchForce = 700f;
    [SerializeField]
    private GameObject cameraTracker;
    public float timer = 0f;
    public float waitingTime = 0.75f;
    public GameObject enemyShip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer = timer + Time.deltaTime;
        //Debug.Log(enemyShip.transform.position.z - 70f);
        if(enemyShip.transform.position.z - 100f <= cameraTracker.transform.position.z)
        {
            timer = timer + Time.deltaTime;
            if(timer > waitingTime)
            {
                timer = 0f;
                EShoot();
            }
        }
    }

    void EShoot()
    {
        //Debug.Log("inside EShoot()");
        var projectileInstance = Instantiate(
            bulletRigidBody,
            firePoint.position,
            firePoint.rotation
        );

        projectileInstance.velocity = transform.TransformDirection(0, 0, 100f);

        foreach(GameObject bulletClone in GameObject.FindObjectsOfType<GameObject>())
            {
                if(bulletClone.name == "Bullet(Clone)")
                {
                    Destroy(bulletClone, 2);
                    
                    if(bulletClone.transform.position.z - cameraTracker.transform.position.z < 0)
                        Destroy(bulletClone);
                }
            }
        
            projectileInstance.AddForce(firePoint.forward.normalized * launchForce);
    }
}
