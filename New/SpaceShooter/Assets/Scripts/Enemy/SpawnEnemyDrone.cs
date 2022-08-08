using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyDrone : MonoBehaviour
{
    [SerializeField] private GameObject enemyDrone;

    private SpawnObjects spawnObjects;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        spawnObjects = GetComponent<SpawnObjects>();
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
    }

    public void Spawn(float playerMoveDirectionX, float playerMoveDirectionY, List<Vector3> positions, List<Vector3> directions)
    {
        // If the player is note moving, instantiate random EnemyDrones at random locations
        if (Mathf.Abs(playerMoveDirectionX) < Properties.PLAYER_MOVE_THRESHOLD && Mathf.Abs(playerMoveDirectionY) < Properties.PLAYER_MOVE_THRESHOLD)
            InstantiateEnemyDroneAtRandomLocation(enemyDrone, positions, directions);

        // Otherwise, instantiate EnemyDrones at specific locations. Eg, if the player is moving upwards, spawn EnemyDrone at the top of the viewport.
        // Likewise, spawn the EnemyDrones across all the eight directions according to the direction of the player.
        else
        {
            SpawnObjects.playerDirectionEnum playerDirection = spawnObjects.GetPlayerDirection();

            switch (playerDirection)
            {
                // Top
                case SpawnObjects.playerDirectionEnum.top:
                    InstantiateEnemyDroneAtNonRandomLocation(enemyDrone, positions[0], directions[4]);
                    break;

                // Top-Right
                case SpawnObjects.playerDirectionEnum.topRight:
                    InstantiateEnemyDroneAtNonRandomLocation(enemyDrone, positions[0], directions[4]);
                    break;

                // Right
                case SpawnObjects.playerDirectionEnum.right:
                    InstantiateEnemyDroneAtNonRandomLocation(enemyDrone, positions[3], directions[6]);
                    break;

                // Down-Right
                case SpawnObjects.playerDirectionEnum.downRight:
                    InstantiateEnemyDroneAtNonRandomLocation(enemyDrone, positions[1], directions[0]);
                    break;

                // Down
                case SpawnObjects.playerDirectionEnum.down:
                    InstantiateEnemyDroneAtNonRandomLocation(enemyDrone, positions[1], directions[0]);
                    break;

                // Down-Left
                case SpawnObjects.playerDirectionEnum.downLeft:
                    InstantiateEnemyDroneAtNonRandomLocation(enemyDrone, positions[1], directions[0]);
                    break;

                // Left
                case SpawnObjects.playerDirectionEnum.left:
                    InstantiateEnemyDroneAtNonRandomLocation(enemyDrone, positions[2], directions[2]);
                    break;

                // Top-Left
                case SpawnObjects.playerDirectionEnum.topLeft:
                    InstantiateEnemyDroneAtNonRandomLocation(enemyDrone, positions[0], directions[4]);
                    break;

                // Not Moving
                case SpawnObjects.playerDirectionEnum.notMoving:
                    InstantiateEnemyDroneAtRandomLocation(enemyDrone, positions, directions);
                    break;
            }
        }
    }

    private void InstantiateEnemyDroneAtRandomLocation(GameObject enemyDrone, List<Vector3> positions, List<Vector3> directions)
    {
        Vector3 randomPosition = positions[Random.Range(0, positions.Count)];
        Vector3 randomDirection = directions[Random.Range(0, directions.Count)];

        Instantiate(enemyDrone, randomPosition, playerTransform.rotation);
    }

    private void InstantiateEnemyDroneAtNonRandomLocation(GameObject enemyDrone, Vector3 position, Vector3 direction)
    {
        Instantiate(enemyDrone, position, playerTransform.rotation);
    }
}
