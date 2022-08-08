using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    [SerializeField] private float powerUpSpeed = 10000f;

    private SpawnObjects spawnObjects;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Awake()
    {
        spawnObjects = GetComponent<SpawnObjects>();
        playerTransform = GameObject.Find(Properties.PLAYER).transform;
    }

    public void Spawn(float playerMoveDirectionX, float playerMoveDirectionY, GameObject randomPowerUp, List<Vector3> positions, List<Vector3> directions)
    {
        // If the player is note moving, instantiate random powerups at random locations
        if (Mathf.Abs(playerMoveDirectionX) < Properties.PLAYER_MOVE_THRESHOLD && Mathf.Abs(playerMoveDirectionY) < Properties.PLAYER_MOVE_THRESHOLD)
            InstantiatePowerUpAtRandomLocation(randomPowerUp, positions, directions);

        // Otherwise, instantiate powerups at specific locations. Eg, if the player is moving upwards, spawn powerup at the top of the viewport.
        // Likewise, spawn the powerups across all the eight directions according to the direction of the player.
        else
        {
            SpawnObjects.playerDirectionEnum playerDirection = spawnObjects.GetPlayerDirection();

            switch (playerDirection)
            {
                // Top
                case SpawnObjects.playerDirectionEnum.top:
                    InstantiatePowerUpAtNonRandomLocation(randomPowerUp, positions[0], directions[4]);
                    break;

                // Top-Right
                case SpawnObjects.playerDirectionEnum.topRight:
                    InstantiatePowerUpAtNonRandomLocation(randomPowerUp, positions[0], directions[4]);
                    break;

                // Right
                case SpawnObjects.playerDirectionEnum.right:
                    InstantiatePowerUpAtNonRandomLocation(randomPowerUp, positions[3], directions[6]);
                    break;

                // Down-Right
                case SpawnObjects.playerDirectionEnum.downRight:
                    InstantiatePowerUpAtNonRandomLocation(randomPowerUp, positions[1], directions[0]);
                    break;

                // Down
                case SpawnObjects.playerDirectionEnum.down:
                    InstantiatePowerUpAtNonRandomLocation(randomPowerUp, positions[1], directions[0]);
                    break;

                // Down-Left
                case SpawnObjects.playerDirectionEnum.downLeft:
                    InstantiatePowerUpAtNonRandomLocation(randomPowerUp, positions[1], directions[0]);
                    break;

                // Left
                case SpawnObjects.playerDirectionEnum.left:
                    InstantiatePowerUpAtNonRandomLocation(randomPowerUp, positions[2], directions[2]);
                    break;

                // Top-Left
                case SpawnObjects.playerDirectionEnum.topLeft:
                    InstantiatePowerUpAtNonRandomLocation(randomPowerUp, positions[0], directions[4]);
                    break;

                // Not Moving
                case SpawnObjects.playerDirectionEnum.notMoving:
                    InstantiatePowerUpAtRandomLocation(randomPowerUp, positions, directions);
                    break;
            }
        }
    }

    private void InstantiatePowerUpAtRandomLocation(GameObject randomPowerUp, List<Vector3> positions, List<Vector3> directions)
    {
        Vector3 randomPosition = positions[Random.Range(0, positions.Count)];
        Vector3 randomDirection = directions[Random.Range(0, directions.Count)];

        GameObject powerUp = Instantiate(randomPowerUp, randomPosition, playerTransform.rotation);

        powerUp.GetComponent<Rigidbody>().AddRelativeForce(playerTransform.position - randomDirection * powerUpSpeed);
    }

    private void InstantiatePowerUpAtNonRandomLocation(GameObject randomPowerUp, Vector3 position, Vector3 direction)
    {
        GameObject powerUp = Instantiate(randomPowerUp, position, playerTransform.rotation);

        powerUp.GetComponent<Rigidbody>().AddRelativeForce(direction * powerUpSpeed);
    }
}
