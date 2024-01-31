using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractorRework : MonoBehaviour
{
    // Progress Bar variables
    public Image progressBar;

    [SerializeField] private float currentProgress = 0.0f, maxProgress = 100.0f;
    [SerializeField] private float lerpSpeed;

    // Task type definition
    private enum Tasks
    { FEED_FISH, EAT, SHOWER };

    private Tasks currentTask;
    private List<Tasks> todolist = new List<Tasks>();

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

    //public ProgressBar progressBar;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        soundManager = GetComponent<SoundManager>();
        textController = GetComponent<TextController>();

        todolist.Add(Tasks.FEED_FISH);
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

            playerMovement.canMove = true;

            currentProgress = 0.0f;
            progressBar.fillAmount = 0;
        }
    }

    private void checkRay(Ray rayCast)
    {
        if (Physics.Raycast(rayCast, out RaycastHit hit, InteractRange, ActionLayer))
        {
            if (hit.collider.CompareTag("FishTank"))
            {
                Debug.Log("Item hit is a fish tank");

                if (!fishDone)
                {
                    progressBar.enabled = true;
                    playerMovement.canMove = false;
                    currentTask = Tasks.FEED_FISH;

                    StartCoroutine("ProgressBar", 0);
                }
            }
            if (hit.collider.CompareTag("Shower"))
            {
                Debug.Log("Item hit is a shower");

                if (!showerDone)
                {
                    progressBar.enabled = true;
                    playerMovement.canMove = false;
                    currentTask = Tasks.SHOWER;

                    StartCoroutine("ProgressBar", 1);
                }
            }
            if (hit.collider.CompareTag("Food"))
            {
                Debug.Log("Item hit is food");

                if (!eatDone)
                {
                    progressBar.enabled = true;
                    playerMovement.canMove = false;
                    currentTask = Tasks.EAT;

                    StartCoroutine("ProgressBar", 2);
                }
            }
            else
            {
                Debug.Log("Item hit is not interactible");
            }
        }
        else
        {
            Debug.Log("No item hit");
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
            lerpSpeed = 3f * Time.deltaTime;

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
                }
                if (index == 1)
                {
                    showerDone = true;
                    Debug.Log("Shower completed");
                }
                if (index == 2)
                {
                    eatDone = true;
                    Debug.Log("Eat completed");
                }

                todolist.Remove(currentTask);

                StopAllCoroutines();
            }

            soundManager.SelectAudio(index, 0.5f);

            yield return new WaitForSeconds(1f);
        }
    }
}