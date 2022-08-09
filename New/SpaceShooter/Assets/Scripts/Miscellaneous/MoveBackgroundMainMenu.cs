using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundMainMenu : MonoBehaviour
{
    [SerializeField] private float parallax = 20;

    private MeshRenderer meshRenderer;
    private Material material;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = material.mainTextureOffset;
        offset.x = offset.x + Time.deltaTime / transform.localScale.x / parallax;

        material.mainTextureOffset = offset;
    }
}
