using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackerMovement : MonoBehaviour
{
    Vector3 position = new Vector3(0f, 0f, -50f);
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        position = new Vector3(0f, 0f, -50f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + transform.forward * speed
        transform.position = new Vector3(position.x, position.y, position.z);
    }
}
