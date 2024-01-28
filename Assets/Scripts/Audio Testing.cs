using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTesting : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            soundManager.SelectAudio(4, 0.5f);
        }
    }
}