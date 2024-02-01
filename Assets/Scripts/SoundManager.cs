using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;

    private AudioSource controlAudio;

    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Index 0 feed fish, 1 footsteps, 2 eat, 4 task completed, 5 wake up, 7 dialogue, 8 shower
    /// </summary>
    /// <param name="index">define sound index</param>
    /// <param name="volume">define sound volume</param>
    public void SelectAudio(int index, float volume)
    {
        if (!controlAudio.isPlaying)
            controlAudio.PlayOneShot(audios[index], volume);
    }

    public void SelectAudio(AudioClip audio, float volume)
    {
        controlAudio.PlayOneShot(audio, volume);
    }

    public void stopAudio()
    {
        if (controlAudio.isPlaying)
        {
            controlAudio.Stop();
        }
    }
}