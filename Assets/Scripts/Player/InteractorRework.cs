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

    //public ProgressBar progressBar;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        soundManager = GetComponent<SoundManager>();

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
            else
            {
                Debug.Log("Item hit is not fish tank");
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

                fishDone = true;
                todolist.Remove(currentTask);

                StopAllCoroutines();
            }

            soundManager.SelectAudio(index, 0.5f);

            yield return new WaitForSeconds(1f);
        }
    }
}