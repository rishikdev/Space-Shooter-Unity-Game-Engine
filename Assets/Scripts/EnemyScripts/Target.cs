using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    EnemyRespawn enemyRespawn;
    //GameObject bullet;
    public float timer = 0f;
    float randomWaitingTime;
    public int hitPoint = 5;
    public int hitsRemaining;
    // Start is called before the first frame update
    void Start()
    {
        enemyRespawn = FindObjectOfType<EnemyRespawn>();
        hitsRemaining = hitPoint;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider bullet)
    {
        
        if(hitsRemaining == 0)
        {
            hitsRemaining = hitPoint;

            if(bullet.gameObject.name == "Bullet(Clone)" || bullet.gameObject.name == "Broad(Clone)")
            {
                Destroy(bullet.gameObject);
                
                randomWaitingTime = Random.Range(100f, 150f);
                
                gameObject.SetActive(false);
                enemyRespawn.Respawn(gameObject);
                RandomTmeDelay(randomWaitingTime);
                gameObject.SetActive(true);
            }
        }
        else
        {
            if(bullet.gameObject.name == "Bullet(Clone)" || bullet.gameObject.name == "Broad(Clone)")
                Destroy(bullet.gameObject);

            hitsRemaining = hitsRemaining - 1;
        }
    }

    void RandomTmeDelay(float randomWaitingTime)
    {
        while(timer <= randomWaitingTime)
        {
            timer = timer + Time.deltaTime;
        }

        timer = 0f;

        return;
    }
}
