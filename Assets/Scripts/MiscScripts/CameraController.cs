using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraTracker;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - cameraTracker.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 xDirection = new Vector3(cameraTracker.transform.position.x, 0f, 0.1f);
        transform.position = offset + cameraTracker.transform.position - xDirection;
        //Debug.Log(player.transform.position.x);
    }
}
