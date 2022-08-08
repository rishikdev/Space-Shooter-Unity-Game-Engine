using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private float powerUpSpawnTime = 1f;
    [SerializeField] private float enemySpawnTime = 1f;

    private Transform playerTransform;
    private Transform screenBoundaryOuter;
    private Transform screenBoundaryInner;

    private GameObject screenBoundaryBottomLeftOuter;
    private GameObject screenBoundaryTopLeftOuter;
    private GameObject screenBoundaryTopRightOuter;
    private GameObject screenBoundaryBottomRightOuter;
    private GameObject screenBoundaryBottomLeftInner;
    private GameObject screenBoundaryTopLeftInner;
    private GameObject screenBoundaryTopRightInner;
    private GameObject screenBoundaryBottomRightInner;

    private Vector2 playerMoveDirection = Vector2.zero;
    private PlayerInputActions playerControls;
    private InputAction playerMove;

    private SpawnPowerUp spawnPowerUp;
    private SpawnEnemyDrone spawnEnemyDrone;

    private int powerUpLimit = 2;
    private int enemyLimit = 5;

    public enum playerDirectionEnum
    {
        top, topRight, right, downRight, down, downLeft,  left, topLeft, notMoving
    }

    void Awake()
    {
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
        screenBoundaryOuter = GameObject.Find(Properties.SCREEN_BOUNDARY_OUTER).transform;
        screenBoundaryInner = GameObject.Find(Properties.SCREEN_BOUNDARY_INNER).transform;

        screenBoundaryBottomLeftOuter = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_LEFT_OUTER);
        screenBoundaryTopLeftOuter = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_LEFT_OUTER);
        screenBoundaryTopRightOuter = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_RIGHT_OUTER);
        screenBoundaryBottomRightOuter = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_RIGHT_OUTER);

        screenBoundaryBottomLeftInner = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_LEFT_INNER);
        screenBoundaryTopLeftInner = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_LEFT_INNER);
        screenBoundaryTopRightInner = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_RIGHT_INNER);
        screenBoundaryBottomRightInner = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_RIGHT_INNER);

        playerControls = new PlayerInputActions();

        SetOuterBounds();
        SetInnerBounds();

        spawnPowerUp = GetComponent<SpawnPowerUp>();
        spawnEnemyDrone = GetComponent<SpawnEnemyDrone>();
    }

    private void OnEnable()
    {
        playerMove = playerControls.Player.Move;
        playerMove.Enable();
    }

    private void OnDisable()
    {
        playerMove.Disable();
    }

    private void Update()
    {
        // Using moveDirection to get the direction towards which the player is moving
        playerMoveDirection = playerMove.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBounds();

        powerUpSpawnTime = powerUpSpawnTime - Time.deltaTime;
        enemySpawnTime = enemySpawnTime - Time.deltaTime;

        if (powerUpSpawnTime <= 0f && GameObject.FindGameObjectsWithTag(Properties.POWERUP_TAG).Length < powerUpLimit)
        {
            SpawnRandomPowerUp();
            powerUpSpawnTime = Random.Range(2, 3);
        }

        if(enemySpawnTime <= 0 && GameObject.FindGameObjectsWithTag(Properties.ENEMY_TAG).Length < enemyLimit)
        {
            SpawnEnemyDrone();
            enemySpawnTime = Random.Range(2, 3);
        }
    }

    private void SetOuterBounds()
    {
        var verticalBound = Camera.main.orthographicSize + Properties.SCREEN_BOUNDARY_OFFSET_OUTER;
        var horizontalBound = (verticalBound * Screen.width / Screen.height) + Properties.SCREEN_BOUNDARY_OFFSET_OUTER;

        screenBoundaryBottomLeftOuter.transform.position = new Vector3(-(horizontalBound - screenBoundaryOuter.position.x), -(verticalBound - screenBoundaryOuter.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryTopLeftOuter.transform.position = new Vector3(-(horizontalBound - screenBoundaryOuter.position.x), (verticalBound + screenBoundaryOuter.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryTopRightOuter.transform.position = new Vector3((horizontalBound + screenBoundaryOuter.position.x), (verticalBound + screenBoundaryOuter.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryBottomRightOuter.transform.position = new Vector3((horizontalBound + screenBoundaryOuter.position.x), -(verticalBound - screenBoundaryOuter.position.y), Properties.PLAYER_Z_POSITION);
    }

    private void SetInnerBounds()
    {
        var verticalBound = Camera.main.orthographicSize + Properties.SCREEN_BOUNDARY_OFFSET_INNER;
        var horizontalBound = (verticalBound * Screen.width / Screen.height) + Properties.SCREEN_BOUNDARY_OFFSET_INNER;

        screenBoundaryBottomLeftInner.transform.position = new Vector3(-(horizontalBound - screenBoundaryInner.position.x), -(verticalBound - screenBoundaryInner.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryTopLeftInner.transform.position = new Vector3(-(horizontalBound - screenBoundaryInner.position.x), (verticalBound + screenBoundaryInner.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryTopRightInner.transform.position = new Vector3((horizontalBound + screenBoundaryInner.position.x), (verticalBound + screenBoundaryInner.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryBottomRightInner.transform.position = new Vector3((horizontalBound + screenBoundaryInner.position.x), -(verticalBound - screenBoundaryInner.position.y), Properties.PLAYER_Z_POSITION);
    }

    private void MoveBounds()
    {
        screenBoundaryOuter.position = new Vector3(playerTransform.position.x, playerTransform.position.y, Properties.PLAYER_Z_POSITION);
        screenBoundaryInner.position = new Vector3(playerTransform.position.x, playerTransform.position.y, Properties.PLAYER_Z_POSITION);
    }

    private List<Vector3> GetPositions()
    {
        Vector3 positionTop = new Vector3(Random.Range(screenBoundaryTopLeftInner.transform.position.x, screenBoundaryTopRightInner.transform.position.x),
                                          Random.Range(screenBoundaryTopLeftOuter.transform.position.y, screenBoundaryTopRightOuter.transform.position.y),
                                          Properties.PLAYER_Z_POSITION);

        Vector3 positionBottom = new Vector3(Random.Range(screenBoundaryBottomLeftInner.transform.position.x, screenBoundaryBottomRightInner.transform.position.x),
                                          Random.Range(screenBoundaryBottomLeftOuter.transform.position.y, screenBoundaryBottomRightOuter.transform.position.y),
                                          Properties.PLAYER_Z_POSITION);

        Vector3 positionLeft = new Vector3(Random.Range(screenBoundaryBottomLeftOuter.transform.position.x, screenBoundaryTopLeftOuter.transform.position.x),
                                          Random.Range(screenBoundaryBottomLeftInner.transform.position.y, screenBoundaryTopLeftInner.transform.position.y),
                                          Properties.PLAYER_Z_POSITION);

        Vector3 positionRight = new Vector3(Random.Range(screenBoundaryBottomRightOuter.transform.position.x, screenBoundaryTopRightOuter.transform.position.x),
                                          Random.Range(screenBoundaryBottomRightInner.transform.position.y, screenBoundaryTopRightInner.transform.position.y),
                                          Properties.PLAYER_Z_POSITION);

        List<Vector3> positions = new()
        {
            positionTop,
            positionBottom,
            positionLeft,
            positionRight
        };

        return positions;
    }

    private List<Vector3> GetDirections()
    {
        List<Vector3> directions = new()
        {
            Vector3.up,
            Vector3.up + Vector3.right,
            Vector3.right,
            Vector3.right + Vector3.down,
            Vector3.down,
            Vector3.down + Vector3.left,
            Vector3.left,
            Vector3.left + Vector3.up
        };

        return directions;
    }

    public playerDirectionEnum GetPlayerDirection()
    {
        float playerMoveDirectionX = playerMoveDirection.x;
        float playerMoveDirectionY = playerMoveDirection.y;

        playerDirectionEnum playerDirection;

        // Top
        if (Mathf.Abs(playerMoveDirectionX) < Properties.PLAYER_MOVE_THRESHOLD && playerMoveDirectionY > Properties.PLAYER_MOVE_THRESHOLD)
            playerDirection = playerDirectionEnum.top;

        // Top-Right
        else if (playerMoveDirectionX > Properties.PLAYER_MOVE_THRESHOLD && playerMoveDirectionY > Properties.PLAYER_MOVE_THRESHOLD)
            playerDirection = playerDirectionEnum.topRight;

        // Right
        else if (playerMoveDirectionX > Properties.PLAYER_MOVE_THRESHOLD && Mathf.Abs(playerMoveDirectionY) < Properties.PLAYER_MOVE_THRESHOLD)
            playerDirection = playerDirectionEnum.right;

        // Down-Right
        else if (playerMoveDirectionX > Properties.PLAYER_MOVE_THRESHOLD && playerMoveDirectionY < -Properties.PLAYER_MOVE_THRESHOLD)
            playerDirection = playerDirectionEnum.downRight;

        // Down
        else if (Mathf.Abs(playerMoveDirectionX) < Properties.PLAYER_MOVE_THRESHOLD && playerMoveDirectionY < -Properties.PLAYER_MOVE_THRESHOLD)
            playerDirection = playerDirectionEnum.down;

        // Down-Left
        else if (playerMoveDirectionX < -Properties.PLAYER_MOVE_THRESHOLD && playerMoveDirectionY < -Properties.PLAYER_MOVE_THRESHOLD)
            playerDirection = playerDirectionEnum.downLeft;

        // Left
        else if (playerMoveDirectionX < -Properties.PLAYER_MOVE_THRESHOLD && Mathf.Abs(playerMoveDirectionY) < Properties.PLAYER_MOVE_THRESHOLD)
            playerDirection = playerDirectionEnum.left;

        // Top-Left
        else if (playerMoveDirectionX < -Properties.PLAYER_MOVE_THRESHOLD && playerMoveDirectionY > Properties.PLAYER_MOVE_THRESHOLD)
            playerDirection = playerDirectionEnum.topLeft;

        // Not Moving
        else
            playerDirection = playerDirectionEnum.notMoving;

        return playerDirection;
    }

    private void SpawnRandomPowerUp()
    {
        GameObject randomPowerUp = powerUps[Random.Range(0, powerUps.Length)];
        List<Vector3> positions = GetPositions();
        List<Vector3> directions = GetDirections();

        float playerMoveDirectionX = playerMoveDirection.x;
        float playerMoveDirectionY = playerMoveDirection.y;

        spawnPowerUp.Spawn(playerMoveDirectionX, playerMoveDirectionY, randomPowerUp, positions, directions);
    }

    private void SpawnEnemyDrone()
    {
        List<Vector3> positions = GetPositions();
        List<Vector3> directions = GetDirections();

        float playerMoveDirectionX = playerMoveDirection.x;
        float playerMoveDirectionY = playerMoveDirection.y;

        spawnEnemyDrone.Spawn(playerMoveDirectionX, playerMoveDirectionY, positions, directions);
    }
}
