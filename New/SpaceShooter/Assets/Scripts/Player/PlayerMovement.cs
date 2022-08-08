using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float playerMoveSpeed = 50f;
    [SerializeField] private float playerRotateSpeed = 150f;

    //private Transform mainCameraTransform;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 lookDirection = Vector2.zero;
    private Transform spaceshipTransform;
    private PlayerInputActions playerControls;
    private InputAction playerMove;
    private InputAction playerLook;
    private InputAction playerFire;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        //mainCameraTransform = GameObject.Find(Properties.MAIN_CAMERA).transform;
    }

    private void OnEnable()
    {
        playerMove = playerControls.Player.Move;
        playerMove.Enable();

        playerLook = playerControls.Player.Look;
        playerLook.Enable();

        playerFire = playerControls.Player.Fire;
        playerFire.Enable();
    }

    private void OnDisable()
    {
        playerMove.Disable();
        playerLook.Disable();
        playerFire.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        spaceshipTransform = GameObject.Find(Properties.SPACESHIP_NAME).transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = playerMove.ReadValue<Vector2>();
        lookDirection = playerLook.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        // This line is used to move the spaceship
        playerRigidbody.AddForce(new Vector2(moveDirection.x * playerMoveSpeed, moveDirection.y * playerMoveSpeed), ForceMode.VelocityChange);

        // The following two lines are used to rotate the spaceship along the y axis when it is moved along the x axis
        //var desiredRotation = Quaternion.Euler(new Vector3(spaceshipTransform.eulerAngles.x,
        //                                                   moveDirection.x < 0 ? -Mathf.Max(moveDirection.x * playerMoveSpeed, -45) : -Mathf.Min(moveDirection.x * playerMoveSpeed, 45),
        //                                                   spaceshipTransform.eulerAngles.z));

        //spaceshipTransform.rotation = Quaternion.Slerp(spaceshipTransform.rotation, desiredRotation, Time.deltaTime);

        // Changing the location of the Main Camera with respect to the player
        //mainCameraTransform.position = gameObject.transform.position;
    }

    private void RotatePlayer()
    {
        // The following two lines are used to rotate the spaceship along the y axis when it is moved along the x axis
        var desiredRotation = Quaternion.Euler(new Vector3(spaceshipTransform.eulerAngles.x,
                                                           spaceshipTransform.eulerAngles.y,
                                                           spaceshipTransform.eulerAngles.z + (lookDirection.x < 0 ? -(lookDirection.x * playerRotateSpeed) : -(lookDirection.x * playerRotateSpeed))));

        spaceshipTransform.rotation = Quaternion.Slerp(spaceshipTransform.rotation, desiredRotation, Time.deltaTime);
    }
}
