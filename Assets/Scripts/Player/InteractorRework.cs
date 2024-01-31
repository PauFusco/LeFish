using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class InteractorRework : MonoBehaviour
{
    // Progress Bar variables
    public Image progressBar;

    [SerializeField] private float currentProgress = 0.0f, maxProgress = 100.0f;
    [SerializeField] private float lerpSpeed;

    // Task type definition
    public enum Tasks
    { FEED_FISH, EAT, SHOWER };

    private Tasks currentTask;
    public List<Tasks> todolist;

    private bool fishDone = false, eatDone = false, showerDone = false;

    // Interact
    [SerializeField] private LayerMask ActionLayer;

    [SerializeField] private float InteractRange;

    // Player Movement
    private PlayerMovement playerMovement;

    // Sound Manager
    private SoundManager soundManager;

    // Text Controller
    private TextController textController;

    // Door Controller
    private DoorScript doorScript;

    // Visual Effects
    [SerializeField] private VisualEffect foodEffect;
    [SerializeField] private VisualEffect waterEffect;

    //public ProgressBar progressBar;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        soundManager = GetComponent<SoundManager>();
        textController = GetComponent<TextController>();

        foodEffect.Stop();
        waterEffect.Stop();

        todolist.Add(Tasks.FEED_FISH);
        todolist.Add(Tasks.EAT);
        todolist.Add(Tasks.SHOWER);
    }

    // Update is called once per frame
    private void Update()
    {
        //Esto da error
        Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKey(KeyCode.E))
        {
            // check type of object hit
            checkRay(rayCast);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            StopAllCoroutines();
            progressBar.enabled = false;

            foodEffect.Stop();
            waterEffect.Stop();

            playerMovement.canMove = true;

            currentProgress = 0.0f;
            progressBar.fillAmount = 0;

            soundManager.stopAudio();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            checkRayDown(rayCast);
        }
    }

    private void checkRay(Ray rayCast)
    {
        if (Physics.Raycast(rayCast, out RaycastHit hit, InteractRange, ActionLayer))
        {
            if (hit.collider.CompareTag("FishTank"))
            {
                if (!fishDone)
                {
                    progressBar.enabled = true;
                    foodEffect.Play();
                    playerMovement.canMove = false;
                    currentTask = Tasks.FEED_FISH;

                    StartCoroutine("ProgressBar", 0);
                }
            }
            if (hit.collider.CompareTag("Shower"))
            {
                if (!showerDone)
                {
                    progressBar.enabled = true;
                    waterEffect.Play();
                    playerMovement.canMove = false;
                    currentTask = Tasks.SHOWER;

                    StartCoroutine("ProgressBar", 1);
                }
            }
            if (hit.collider.CompareTag("Food"))
            {
                if (!eatDone)
                {
                    progressBar.enabled = true;
                    playerMovement.canMove = false;
                    currentTask = Tasks.EAT;

                    StartCoroutine("ProgressBar", 2);
                }
            }
        }
        else
        {
            Debug.Log("No item hit");
        }
    }

    private void checkRayDown(Ray rayCast)
    {
        if (Physics.Raycast(rayCast, out RaycastHit hit, InteractRange, ActionLayer))
        {
            if (hit.collider.CompareTag("Door"))
            {
                doorScript = hit.transform.gameObject.GetComponent<DoorScript>();

                doorScript.doorInteract();
                //if (!doorOpen)
                //{
                //    doorAnim.Play("DoorOpen", 0, 0.0f);
                //    doorOpen = true;
                //}
                //else
                //{
                //    doorAnim.Play("DoorClose", 0, 0.0f);
                //    doorOpen = false;
                //}
            }
        }
    }

    /// <summary>
    /// enter sound index to play
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private IEnumerator ProgressBar(int index)
    {
        while (currentProgress <= maxProgress)
        {
            lerpSpeed = 1;

            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, currentProgress / maxProgress, lerpSpeed);
            currentProgress += 0.1f;

            Color healthColor = Color.Lerp(Color.red, Color.green, (currentProgress / maxProgress));
            progressBar.color = healthColor;

            if (currentProgress >= maxProgress)
            {
                progressBar.enabled = false;

                playerMovement.canMove = true;

                currentProgress = 0.0f;
                progressBar.fillAmount = 0;

                if (index == 0)
                {
                    fishDone = true;
                    Debug.Log("Fish completed");
                    foodEffect.Stop();
                }
                if (index == 1)
                {
                    showerDone = true;
                    Debug.Log("Shower completed");
                    waterEffect.Stop();
                }
                if (index == 2)
                {
                    eatDone = true;
                    Debug.Log("Eat completed");
                }

                soundManager.stopAudio();

                soundManager.SelectAudio(4, 0.5f);

                todolist.Remove(currentTask);

                StopAllCoroutines();
            }

            soundManager.SelectAudio(index, 0.5f);

            yield return new WaitForSeconds(1f);
        }
    }
}