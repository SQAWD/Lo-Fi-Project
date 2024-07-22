using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterAnimation : MonoBehaviour
{
    public Texture2D firstFrame;
    public Texture2D[] frames;
    public float framesPerSecond = 30.0f;

    private Renderer rend;
    private int frameIndex;
    private float timer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        frameIndex = 0;

        if (firstFrame != null)
        {
            rend.material.mainTexture = firstFrame;
        }
        else if (frames.Length > 0)
        {
            rend.material.mainTexture = frames[0];
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1.0f / framesPerSecond)
        {
            timer -= 1.0f / framesPerSecond;
            frameIndex = (frameIndex + 1) % frames.Length;
            rend.material.mainTexture = frames[frameIndex];
        }
    }
}
