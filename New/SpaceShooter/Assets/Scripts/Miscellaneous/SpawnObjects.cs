using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnObjects : MonoBehaviour
{
    /*******************************************
     * index 0 - single bullet                 *
     * index 1 - triple bullets                *
     * index 2 - spread                        *
     * index 3 - laser                         *
     * index 4 - shield                        *
     *******************************************/
    [SerializeField] private GameObject[] powerUps;

    /******************************************************
     * index 0 - enemy rammer                             *
     * index 1 - drone with bullets                       *
     * index 2 - drone with lasers                        *
     * index 3 - level 1 boss                             *
     ******************************************************/
    [SerializeField] private GameObject[] enemyGameObjects;

    [SerializeField] private float powerUpSpawnTime = 10f;
    [SerializeField] private float enemySpawnTime = 10f;
    [SerializeField] private float bossSpawnTime = 5f;

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
    private SpawnEnemy spawnEnemy;

    private int powerUpLimit = 2;
    private int enemyLimit = 10;
    private int innerBoundsVerticalOffset = 100;

    private bool hasBossSpawned;
    public bool spawnBoss;

    private Enemy enemy;
    private UI ui;


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
        spawnEnemy = GetComponent<SpawnEnemy>();

        enemy = GameObject.Find(Properties.ENEMY).GetComponent<Enemy>();
        ui = GameObject.Find(Properties.UI_CANVAS).GetComponent<UI>();

        hasBossSpawned = false;
        ui.Image_BossHealthBar.transform.parent.gameObject.SetActive(false);

        spawnBoss = false;
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
        if(!enemy.isBossActive)
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
            SpawnEnemy();
            enemySpawnTime = Random.Range(1, 3);
        }

        if(spawnBoss)
        {
            bossSpawnTime = bossSpawnTime - Time.deltaTime;

            if(bossSpawnTime <= 0)
            {
                if(!hasBossSpawned)
                    SpawnBoss();
            }
                
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

        screenBoundaryBottomLeftInner.transform.position = new Vector3(-(horizontalBound - screenBoundaryInner.position.x), -(verticalBound - innerBoundsVerticalOffset - screenBoundaryInner.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryTopLeftInner.transform.position = new Vector3(-(horizontalBound - screenBoundaryInner.position.x), (verticalBound - innerBoundsVerticalOffset + screenBoundaryInner.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryTopRightInner.transform.position = new Vector3((horizontalBound + screenBoundaryInner.position.x), (verticalBound - innerBoundsVerticalOffset + screenBoundaryInner.position.y), Properties.PLAYER_Z_POSITION);
        screenBoundaryBottomRightInner.transform.position = new Vector3((horizontalBound + screenBoundaryInner.position.x), -(verticalBound - innerBoundsVerticalOffset - screenBoundaryInner.position.y), Properties.PLAYER_Z_POSITION);
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

    private void SpawnEnemy()
    {
        List<Vector3> positions = GetPositions();

        float playerMoveDirectionX = playerMoveDirection.x;
        float playerMoveDirectionY = playerMoveDirection.y;

        // If the player has killed 5 rammers or 5 drones with bullets, start spawning drones with lasers as well
        if (enemy.GetEnemyWithBulletsKilledCount() <= 5 || enemy.GetEnemyRammerKilledCount() <= 5)    // <= 5
        {
            spawnEnemy.Spawn(enemyGameObjects[0], playerMoveDirectionX, playerMoveDirectionY, positions);
            spawnEnemy.Spawn(enemyGameObjects[1], playerMoveDirectionX, playerMoveDirectionY, positions);
        }

        else
        {
            // If the player has killed 15 drones with lasers and an additional 10 drones with bullets, spawn the boss
            if (enemy.GetEnemyWithLasersKilledCount() <= 15 && enemy.GetEnemyWithBulletsKilledCount() <= 15)  //<= 15, <= 15
            {
                if (Random.Range(0, 10) <= 3)
                {
                    spawnEnemy.Spawn(enemyGameObjects[0], playerMoveDirectionX, playerMoveDirectionY, positions);
                    spawnEnemy.Spawn(enemyGameObjects[1], playerMoveDirectionX, playerMoveDirectionY, positions);
                }

                else
                    spawnEnemy.Spawn(enemyGameObjects[2], playerMoveDirectionX, playerMoveDirectionY, positions);
            }

            else
            {
                if (GameObject.FindGameObjectsWithTag(Properties.ENEMY_TAG).Length == 0)
                {
                    spawnBoss = true;
                }
            }
        }
    }

    private void SpawnBoss()
    {
        hasBossSpawned = true;
        SpawnShieldPowerUp();

        enemy.ActivateBoss();
        hasBossSpawned = true;
        spawnEnemy.SpawnBoss(enemyGameObjects[3]);
        ui.Image_BossHealthBar.transform.parent.gameObject.SetActive(true);
    }

    private void SpawnShieldPowerUp()
    {
        GameObject shieldPowerUp = powerUps[4];
        List<Vector3> directions = GetDirections();

        spawnPowerUp.SpawnShieldBeforeBossBattle(shieldPowerUp, screenBoundaryBottomRightOuter.transform.position.x, directions);
    }
}
