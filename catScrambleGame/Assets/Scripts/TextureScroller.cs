using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public float speed = .5f;

    Renderer renderer;
    float offset;

    private void Start()
    {
        renderer = GetComponent<Renderer>();

    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
        if (offset > 1)
            offset -= 1;
        renderer.material.mainTextureOffset = new Vector2(0, offset);
    }
}