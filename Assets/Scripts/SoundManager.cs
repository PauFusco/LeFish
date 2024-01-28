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
    /// 0 Task, 1 open door, 2 locked door, 3 text sound
    /// </summary>
    /// <param name="index">define sound index</param>
    /// <param name="volume">define sound volume</param>
    public void SelectAudio(int index, float volume)
    {
        controlAudio.PlayOneShot(audios[index], volume);
    }
}