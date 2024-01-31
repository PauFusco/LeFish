using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AudioClip doorInteractAudio;
    private AudioSource audioSource;

    private bool doorOpen = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void doorInteract()
    {
        if (!doorOpen)
        {
            audioSource.PlayOneShot(doorInteractAudio);
            animator.Play("DoorOpen", 0, 0.0f);
            doorOpen = true;
        }
        else
        {
            audioSource.PlayOneShot(doorInteractAudio);
            animator.Play("DoorClose", 0, 0.0f);
            doorOpen = false;
        }
    }
}