using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStraightMovement : MonoBehaviour
{
    EnemyRespawn enemyRespawn;
    public GameObject cameraTracker;
    public float timer = 0f;
    public float waitingTime = 1f;
    bool activateTimer = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyRespawn = FindObjectOfType<EnemyRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activateTimer)
            timer = timer + Time.deltaTime;
        
        if(timer > waitingTime)
        {
            activateTimer = false;
            EnemyMovement();
        }
    }

    public void EnemyMovement()
    {
        transform.Rotate(0f, 0f, 90 * Time.deltaTime);
        transform.position = transform.position + transform.forward * 0.5f;

        if(transform.position.z -110 <= cameraTracker.transform.position.z)
            transform.position = transform.position + transform.forward;
        
        if(transform.position.z < (cameraTracker.transform.position.z - 30))
        {
            timer = 0f;
            activateTimer = true;

            gameObject.SetActive(false);
            enemyRespawn.Respawn(gameObject);
            gameObject.SetActive(true);

            waitingTime = Random.Range(1f, 10f);
        }
    }
}
