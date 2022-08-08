using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerCollide : MonoBehaviour
{
    EnemyRespawn enemyRespawn;
    // Start is called before the first frame update
    void Start()
    {
        enemyRespawn = FindObjectOfType<EnemyRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.name == "Intergalactic Spaceship")
        {
            gameObject.SetActive(false);
            enemyRespawn.Respawn(gameObject);
            gameObject.SetActive(true);
        }
    }
}
