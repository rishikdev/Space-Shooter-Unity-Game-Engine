using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float playerMoveSpeed = 50f;
    [SerializeField] private float playerRotateSpeed = 150f;

    public static SphereCollider sphereCollider;

    private GameObject screenBoundaryBottomLeft;
    private GameObject screenBoundaryTopLeft;
    private GameObject screenBoundaryTopRight;
    private GameObject screenBoundaryBottomRight;

    private Transform mainCameraTransform;
    private Vector2 moveDirection = Vector2.zero;
    private Vector2 lookDirection = Vector2.zero;
    private Vector2 lookDirectionKeyboard = Vector2.zero;
    private Transform spaceshipTransform;
    private PlayerInputActions playerControls;
    private InputAction playerMove;
    private InputAction playerLook;
    private InputAction playerRotateLeft;
    private InputAction playerRotateRight;
    private Enemy enemy;
    private SpawnObjects spawnObjects;
    private bool isPlayerRotating;

    public bool isPlayerInBossPosition;
    public static bool isPlayerStunned;

    private void Awake()
    {
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        sphereCollider.enabled = false;

        screenBoundaryBottomLeft = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_LEFT_INNER);
        screenBoundaryTopLeft = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_LEFT_INNER);
        screenBoundaryTopRight = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_RIGHT_INNER);
        screenBoundaryBottomRight = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_RIGHT_INNER);

        playerControls = new PlayerInputActions();
        enemy = GameObject.Find(Properties.ENEMY).GetComponent<Enemy>();
        spawnObjects = GameObject.Find(Properties.SCREEN_BOUNDARY_OUTER).GetComponent<SpawnObjects>();
        isPlayerRotating = false;
        isPlayerInBossPosition = false;
        isPlayerStunned = false;

        mainCameraTransform = GameObject.Find(Properties.MAIN_CAMERA).transform;
    }

    private void OnEnable()
    {
        playerMove = playerControls.Player.Move;
        playerMove.Enable();

        playerLook = playerControls.Player.Look;
        playerLook.Enable();

        playerRotateLeft = playerControls.Player.RotateLeftKeyboard;
        playerRotateLeft.Enable();

        playerRotateRight = playerControls.Player.RotateRightKeyboard;
        playerRotateRight.Enable();

        // Rotating player left when user presses ',' button (<)
        playerRotateLeft.started += StartLeftRotation;
        
        playerRotateLeft.canceled += StopLeftRotation;

        // Rotating player right when user presses '.' button (>)
        playerRotateRight.started += StartRightRotation;

        playerRotateRight.canceled += StopRightRotation;
    }

    private void OnDisable()
    {
        playerMove.Disable();
        playerLook.Disable();
        playerRotateLeft.Disable();
        playerRotateRight.Disable();
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
        RestrictPlayerPosition();

        if (isPlayerRotating)
            RotatePlayerUsingKeyboard();

        if (!isPlayerStunned)
        {
            if (!spawnObjects.spawnBoss)
                MovePlayer();

            else
            {
                if (!isPlayerInBossPosition)
                    GetPlayerInBossPosition();

                else
                    MovePlayerWithRestrictions();
            }
        }

        RotatePlayer();
    }

    private void MovePlayer()
    {
        playerRigidbody.AddForce(new Vector2(moveDirection.x * playerMoveSpeed, moveDirection.y * playerMoveSpeed), ForceMode.VelocityChange);
    }

    private void StartLeftRotation(InputAction.CallbackContext callbackContext)
    {
        lookDirectionKeyboard = new Vector2(-1, 0);
        isPlayerRotating = true;
    }

    private void StopLeftRotation(InputAction.CallbackContext callbackContext)
    {
        lookDirectionKeyboard = Vector2.zero;
        isPlayerRotating = false;
    }

    private void StartRightRotation(InputAction.CallbackContext callbackContext)
    {
        lookDirectionKeyboard = new Vector2(1, 0);
        isPlayerRotating = true;
    }

    private void StopRightRotation(InputAction.CallbackContext callbackContext)
    {
        lookDirectionKeyboard = Vector2.zero;
        isPlayerRotating = false;
    }

    private void RotatePlayer()
    {
        // The following two lines are used to rotate the spaceship along the y axis when it is moved along the x axis
        var desiredRotation = Quaternion.Euler(new Vector3(spaceshipTransform.eulerAngles.x,
                                                           spaceshipTransform.eulerAngles.y,
                                                           spaceshipTransform.eulerAngles.z + (lookDirection.x < 0 ? -(lookDirection.x * playerRotateSpeed) : -(lookDirection.x * playerRotateSpeed))));

        spaceshipTransform.rotation = Quaternion.Slerp(spaceshipTransform.rotation, desiredRotation, Time.deltaTime);
    }

    private void RotatePlayerUsingKeyboard()
    {
        // The following two lines are used to rotate the spaceship along the y axis when it is moved along the x axis
        var desiredRotation = Quaternion.Euler(new Vector3(spaceshipTransform.eulerAngles.x,
                                                           spaceshipTransform.eulerAngles.y,
                                                           spaceshipTransform.eulerAngles.z + (lookDirectionKeyboard.x < 0 ? -(lookDirectionKeyboard.x * playerRotateSpeed) : -(lookDirectionKeyboard.x * playerRotateSpeed))));

        spaceshipTransform.rotation = Quaternion.Slerp(spaceshipTransform.rotation, desiredRotation, Time.deltaTime);
    }

    private void GetPlayerInBossPosition()
    {
        enemy.ActivateBoss();

        //sphereCollider.enabled = true;

        Vector3 desiredPosition = new Vector3((screenBoundaryBottomLeft.transform.position.x + screenBoundaryBottomRight.transform.position.x) / 2,
                                               screenBoundaryBottomLeft.transform.position.y,
                                               Properties.PLAYER_Z_POSITION);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 0.5f);

        isPlayerInBossPosition = Vector3.Distance(transform.position, desiredPosition) < 50;
    }

    private void MovePlayerWithRestrictions()
    {
        playerRigidbody.AddForce(new Vector2(moveDirection.x * playerMoveSpeed, moveDirection.y * playerMoveSpeed), ForceMode.VelocityChange);

        RestrictPlayerPosition();
    }

    private void RestrictPlayerPosition()
    {
        if (transform.position.x > screenBoundaryBottomRight.transform.position.x)
            transform.position = new Vector3(screenBoundaryBottomRight.transform.position.x, transform.position.y, Properties.PLAYER_Z_POSITION);

        if (transform.position.x < screenBoundaryBottomLeft.transform.position.x)
            transform.position = new Vector3(screenBoundaryBottomLeft.transform.position.x, transform.position.y, Properties.PLAYER_Z_POSITION);

        if (transform.position.y > screenBoundaryTopRight.transform.position.y)
            transform.position = new Vector3(transform.position.x, screenBoundaryTopRight.transform.position.y, Properties.PLAYER_Z_POSITION);

        if (transform.position.y < screenBoundaryBottomLeft.transform.position.y)
            transform.position = new Vector3(transform.position.x, screenBoundaryBottomLeft.transform.position.y, Properties.PLAYER_Z_POSITION);
    }

    private void MovePlayerWithRotation()
    {
        // This line is used to move the spaceship
        playerRigidbody.AddForce(new Vector2(moveDirection.x * playerMoveSpeed, moveDirection.y * playerMoveSpeed), ForceMode.VelocityChange);

        // The following two lines are used to rotate the spaceship along the y axis when it is moved along the x axis
        var desiredRotation = Quaternion.Euler(new Vector3(spaceshipTransform.eulerAngles.x,
                                                           moveDirection.x < 0 ? -Mathf.Max(moveDirection.x * playerMoveSpeed, -45) : -Mathf.Min(moveDirection.x * playerMoveSpeed, 45),
                                                           spaceshipTransform.eulerAngles.z));

        spaceshipTransform.rotation = Quaternion.Slerp(spaceshipTransform.rotation, desiredRotation, Time.deltaTime);

        // Changing the location of the Main Camera with respect to the player
        mainCameraTransform.position = gameObject.transform.position;
    }
}
