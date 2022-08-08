using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpeed : MonoBehaviour
{
    public GameObject cameraTracker;
    public GameObject player;
    float newPositionZ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position - transform.forward * 0.35f;
        
        if(transform.position.x >= 38)
            transform.position = new Vector3(38f, transform.position.y, transform.position.z);

        if(transform.position.x <= -38)
            transform.position = new Vector3(-38f, transform.position.y, transform.position.z);
    }

    public void IsPlayerMovingInX(float horizontal)
    {
        if(horizontal > 0f)
            transform.position = new Vector3(transform.position.x + 0.35f, transform.position.y, transform.position.z);

        if(horizontal < 0f)
            transform.position = new Vector3(transform.position.x - 0.35f, transform.position.y, transform.position.z);
    }
}
