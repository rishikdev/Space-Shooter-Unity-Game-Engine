using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCollide : MonoBehaviour
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

    public void OnTriggerEnter(Collider enemy)
    {
        if(enemy.gameObject.name.Contains("Enemy"))
        {
            powerUpSpread.useSpread = false;
            powerUpBroad.useBroad = false;

            gameObject.SetActive(false);
            transform.position = cameraTracker.transform.position;
            gameObject.SetActive(true);
        }
    }
}
