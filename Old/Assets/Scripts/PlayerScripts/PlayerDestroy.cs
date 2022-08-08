using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
    private PowerUpSpread powerUpSpread;
    private PowerUpBroad powerUpBroad;
    public GameObject cameraTracker;
    // Start is called before the first frame update
    void Start()
    {
        powerUpSpread = FindObjectOfType<PowerUpSpread>();
        powerUpBroad = FindObjectOfType<PowerUpBroad>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider enemyBullet)
    {
        if(enemyBullet.gameObject.name == "EnemyBullet(Clone)")
        {
            Destroy(enemyBullet);
            
            powerUpSpread.useSpread = false;
            powerUpBroad.useBroad = false;
            
            gameObject.SetActive(false);
            transform.position = cameraTracker.transform.position;
            gameObject.SetActive(true);
        }
    }
}
