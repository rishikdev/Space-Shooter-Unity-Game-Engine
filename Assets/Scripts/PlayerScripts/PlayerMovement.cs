using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private BackgroundSpeed backgroundSpeed;
    public CharacterController controller;
    public GameObject player;
    public GameObject cameraTracker;
    public float speed = 2f;
    private Joystick joystick;
    private Rigidbody rb;
    private Vector3 inputVector;
    public float adjustValue = 0.9835f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        backgroundSpeed = FindObjectOfType<BackgroundSpeed>();
        joystick = FindObjectOfType<Joystick>();   
    }
    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        
        inputVector = new Vector3(horizontal * speed, 0f, vertical + speed).normalized;
        transform.LookAt(transform.position + new Vector3(0f, 0f, Mathf.Abs(inputVector.z)));
        transform.Rotate(new Vector3(0f, 0f, -horizontal * 45));

        backgroundSpeed.IsPlayerMovingInX(horizontal);
        

        if(vertical != 0)
        {
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            //transform.position = transform.position + transform.forward * speed * adjustValue + direction * speed;
            transform.position = transform.position + direction * speed;
        }

        if(player.transform.position.x >= 35)
            player.transform.position = new Vector3(35f, player.transform.position.y, player.transform.position.z);

        if(player.transform.position.x <= -35)
            player.transform.position = new Vector3(-35f, player.transform.position.y, player.transform.position.z);
        
        if(player.transform.position.z - cameraTracker.transform.position.z > 140)
            player.transform.position = new Vector3(player.transform.position.x, 0f, cameraTracker.transform.position.z + 140);
        
        if(player.transform.position.z <= cameraTracker.transform.position.z)
            player.transform.position = new Vector3(player.transform.position.x, 0f, cameraTracker.transform.position.z);
    }

    void FixedUpdate()
    {
        //rb.velocity = inputVector;
    }
}
