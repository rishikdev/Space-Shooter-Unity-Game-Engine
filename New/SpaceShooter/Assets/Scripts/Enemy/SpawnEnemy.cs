using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private GameObject screenBoundaryTopLeft;

    private SpawnObjects spawnObjects;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        screenBoundaryTopLeft = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_LEFT_OUTER);

        spawnObjects = GetComponent<SpawnObjects>();
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
    }

    public void Spawn(GameObject enemy, float playerMoveDirectionX, float playerMoveDirectionY, List<Vector3> positions)
    {
        // If the player is note moving, instantiate random EnemyDrones at random locations
        if (Mathf.Abs(playerMoveDirectionX) < Properties.PLAYER_MOVE_THRESHOLD && Mathf.Abs(playerMoveDirectionY) < Properties.PLAYER_MOVE_THRESHOLD)
            InstantiateEnemyAtRandomLocation(enemy, positions);

        // Otherwise, instantiate EnemyDrones at specific locations. Eg, if the player is moving upwards, spawn EnemyDrone at the top of the viewport.
        // Likewise, spawn the EnemyDrones across all the eight directions according to the direction of the player.
        else
        {
            SpawnObjects.playerDirectionEnum playerDirection = spawnObjects.GetPlayerDirection();

            switch (playerDirection)
            {
                // Top
                case SpawnObjects.playerDirectionEnum.top:
                    InstantiateEnemyAtNonRandomLocation(enemy, positions[0]);
                    break;

                // Top-Right
                case SpawnObjects.playerDirectionEnum.topRight:
                    InstantiateEnemyAtNonRandomLocation(enemy, positions[0]);
                    break;

                // Right
                case SpawnObjects.playerDirectionEnum.right:
                    InstantiateEnemyAtNonRandomLocation(enemy, positions[3]);
                    break;

                // Down-Right
                case SpawnObjects.playerDirectionEnum.downRight:
                    InstantiateEnemyAtNonRandomLocation(enemy, positions[1]);
                    break;

                // Down
                case SpawnObjects.playerDirectionEnum.down:
                    InstantiateEnemyAtNonRandomLocation(enemy, positions[1]);
                    break;

                // Down-Left
                case SpawnObjects.playerDirectionEnum.downLeft:
                    InstantiateEnemyAtNonRandomLocation(enemy, positions[1]);
                    break;

                // Left
                case SpawnObjects.playerDirectionEnum.left:
                    InstantiateEnemyAtNonRandomLocation(enemy, positions[2]);
                    break;

                // Top-Left
                case SpawnObjects.playerDirectionEnum.topLeft:
                    InstantiateEnemyAtNonRandomLocation(enemy, positions[0]);
                    break;

                // Not Moving
                case SpawnObjects.playerDirectionEnum.notMoving:
                    InstantiateEnemyAtRandomLocation(enemy, positions);
                    break;
            }
        }
    }

    public void SpawnBoss(GameObject bossGameObject)
    {
        InstantiateBoss(bossGameObject);
    }

    private void InstantiateEnemyAtRandomLocation(GameObject enemyDrone, List<Vector3> positions)
    {
        Vector3 randomPosition = positions[Random.Range(0, positions.Count)];

        Instantiate(enemyDrone, randomPosition, playerTransform.rotation);
    }

    private void InstantiateEnemyAtNonRandomLocation(GameObject enemyDrone, Vector3 position)
    {
        Instantiate(enemyDrone, position, playerTransform.rotation);
    }

    private void InstantiateBoss(GameObject boss)
    {
        Vector3 position = new Vector3(playerTransform.position.x, screenBoundaryTopLeft.transform.position.y, Properties.PLAYER_Z_POSITION);
        Instantiate(boss, position, playerTransform.rotation);
    }
}
