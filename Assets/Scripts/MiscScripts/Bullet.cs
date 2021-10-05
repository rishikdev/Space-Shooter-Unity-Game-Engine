using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject cameraTracker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyBullet();
        DestroyEnemyBullet();
    }

    public void DestroyBullet()
    {
        foreach(GameObject playerProjectileClone in GameObject.FindObjectsOfType<GameObject>())
            {
                if(playerProjectileClone.name == "Bullet(Clone)" || playerProjectileClone.name == "Broad(Clone)")
                {
                    //Destroy(bulletClone, 2);
                    
                    if(playerProjectileClone.transform.position.z - cameraTracker.transform.position.z > 180)
                        Destroy(playerProjectileClone);
                }
            }
    }

    public void DestroyEnemyBullet()
    {
        foreach(GameObject bulletClone in GameObject.FindObjectsOfType<GameObject>())
            {
                if(bulletClone.name == "EnemyBullet(Clone)")
                {
                    //Destroy(bulletClone, 2);
                    
                    if(bulletClone.transform.position.z < (cameraTracker.transform.position.z - 30))
                        Destroy(bulletClone);
                }
            }
    }
}
