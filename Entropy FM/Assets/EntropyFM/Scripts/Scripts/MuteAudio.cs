using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private bool muted = false;

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
    }

    public void MuteToggle()
    {
        if (audioSource == null)
        {
            return;
        }

        // Toggle the muted state
        muted = !muted;

        // Set the volume based on the muted state
        audioSource.volume = muted ? 0f : 1f;
    }
}
