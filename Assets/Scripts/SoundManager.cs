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
    /// </summary>
    /// <param name="index">define sound index</param>
    /// <param name="volume">define sound volume</param>
    public void SelectAudio(int index, float volume)
    {
        controlAudio.PlayOneShot(audios[index], volume);
    }

    public void SelectAudio(AudioClip audio, float volume)
    {
        controlAudio.PlayOneShot(audio, volume);
    }
}