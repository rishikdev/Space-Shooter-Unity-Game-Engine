using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private GameObject screenBoundaryBottomLeft;
    private GameObject screenBoundaryTopLeft;
    private GameObject screenBoundaryTopRight;
    private GameObject screenBoundaryBottomRight;
    private Transform playerTransform;

    void Awake()
    {
        screenBoundaryBottomLeft = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_LEFT_OUTER);
        screenBoundaryTopLeft = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_LEFT_OUTER);
        screenBoundaryTopRight = GameObject.Find(Properties.SCREEN_BOUNDARY_TOP_RIGHT_OUTER);
        screenBoundaryBottomRight = GameObject.Find(Properties.SCREEN_BOUNDARY_BOTTOM_RIGHT_OUTER);

        playerTransform = GameObject.Find(Properties.PLAYER).transform;
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
        if (other.name == Properties.SPACESHIP_NAME && gameObject.tag == Properties.ENEMY_BULLET_TAG)
            Destroy(gameObject);

        if (other.name == Properties.SPACESHIP_NAME && gameObject.tag == Properties.ENEMY_STUN_BULLET_TAG)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, 2);
        }
    }
}
