using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private GameObject screenBoundaryBottomLeft;
    private GameObject screenBoundaryTopLeft;
    private GameObject screenBoundaryTopRight;
    private GameObject screenBoundaryBottomRight;

    void Awake()
    {
        screenBoundaryBottomLeft = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_LEFT_OUTER);
        screenBoundaryTopLeft = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_LEFT_OUTER);
        screenBoundaryTopRight = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_RIGHT_OUTER);
        screenBoundaryBottomRight = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_RIGHT_OUTER);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyGameObject(gameObject);
    }

    private void DestroyGameObject(GameObject gameobject)
    {
        if (gameobject.transform.position.x > screenBoundaryTopRight.transform.position.x ||
            gameobject.transform.position.x < screenBoundaryTopLeft.transform.position.x ||
            gameobject.transform.position.y > screenBoundaryTopRight.transform.position.y ||
            gameobject.transform.position.y < screenBoundaryBottomRight.transform.position.y)
            Destroy(gameobject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player's spaceship collided with the enemy or enemy bullet, destroy both objects and instantiate player
        if(other.name == Properties.SPACESHIP_NAME && (gameObject.tag == Properties.ENEMY_BULLET_TAG || gameObject.tag == Properties.ENEMY_TAG))
        {
            Destroy(gameObject);
            //GameObject.FindGameObjectWithTag(Properties.PLAYER).SetActive(false);
        }
    }
}
