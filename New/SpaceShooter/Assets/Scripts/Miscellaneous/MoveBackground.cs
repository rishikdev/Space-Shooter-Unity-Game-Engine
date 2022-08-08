using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private float parallax = 2;

    private MeshRenderer meshRenderer;
    private Material material;
    private Vector2 offset;

    private void Awake()
    {
        playerGameObject = GameObject.Find(Properties.PLAYER);
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        // Changing the offset of the material
        offset = material.mainTextureOffset;
        offset.x = playerGameObject.transform.position.x / transform.localScale.x / parallax;
        offset.y = playerGameObject.transform.position.y / transform.localScale.y / parallax;

        material.mainTextureOffset = offset;

        // Setting the position of the background to match the position of the player
        transform.position = new Vector3(playerGameObject.transform.position.x,
                                         playerGameObject.transform.position.y,
                                         Properties.BACKGROUND_Z_POSITION);
    }
}
