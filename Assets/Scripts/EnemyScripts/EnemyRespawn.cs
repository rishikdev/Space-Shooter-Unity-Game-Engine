using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject cameraTracker;
    //public float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn(GameObject destroyedEnemy)
    {
        {
            destroyedEnemy.transform.position = new Vector3(
                                                    Random.Range(cameraTracker.transform.position.x - 35, cameraTracker.transform.position.x + 35),
                                                    0f,
                                                    cameraTracker.transform.position.z + 180f);
        }
    }
}
