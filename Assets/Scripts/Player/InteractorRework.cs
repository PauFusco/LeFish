using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;
using TMPro;

public class InteractorRework : MonoBehaviour
{
    // Progress Bar variables
    public Image progressBar;

    DialogueManager dialogueManager;

    public TextMeshProUGUI showInteractText;
    public TextMeshProUGUI showShortInteractText;

    [SerializeField] private float currentProgress = 0.0f, maxProgress = 100.0f;
    [SerializeField] private float lerpSpeed;

    // Task type definition
    public enum Tasks
    { FEED_FISH, EAT, SHOWER };

    private Tasks currentTask;
    public List<Tasks> todolist;

    private bool fishDone, eatDone, showerDone;

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

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        soundManager = GetComponent<SoundManager>();
        textController = GetComponent<TextController>();
        dialogueManager = GetComponent<DialogueManager>();

        foodEffect.Stop();
        waterEffect.Stop();

        fishDone = false; showerDone = false; eatDone = false;

        //todolist.Add(Tasks.FEED_FISH);
        //todolist.Add(Tasks.EAT);
        //todolist.Add(Tasks.SHOWER);

        soundManager.SelectAudio(5, 0.5f);
    }

    // Update is called once per frame
    private void Update()
    {
        //Esto da error
        Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);

        CheckRay(rayCast);

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
        
    }

    private void CheckRay(Ray rayCast)
    {
        if (Physics.Raycast(rayCast, out RaycastHit hit, InteractRange, ActionLayer))
        {
            if (hit.collider.CompareTag("FishTank"))
            {
                if (!fishDone && todolist.Contains(Tasks.FEED_FISH))
                {
                    showInteractText.enabled = true;
                    if (Input.GetKey(KeyCode.E))
                    {
                        progressBar.enabled = true;
                        foodEffect.Play();
                        playerMovement.canMove = false;
                        currentTask = Tasks.FEED_FISH;
                        StartCoroutine("ProgressBar", 0);

                    }
                }
            }
            if (hit.collider.CompareTag("Shower"))
            {
                if (!showerDone && todolist.Contains(Tasks.SHOWER))
                {
                    showInteractText.enabled = true;
                    if (Input.GetKey(KeyCode.E))
                    {
                        progressBar.enabled = true;
                        waterEffect.Play();
                        playerMovement.canMove = false;
                        currentTask = Tasks.SHOWER;

                        StartCoroutine("ProgressBar", 1);
                    }
                }
            }
            if (hit.collider.CompareTag("Food"))
            {
                if (!eatDone && todolist.Contains(Tasks.EAT))
                {
                    showInteractText.enabled = true;
                    if (Input.GetKey(KeyCode.E))
                    {
                        progressBar.enabled = true;
                        playerMovement.canMove = false;
                        currentTask = Tasks.EAT;

                        StartCoroutine("ProgressBar", 2);
                    }
                }
            }
            if (hit.collider.CompareTag("Bed"))
            {
                showShortInteractText.enabled = true;
                if (todolist.Count <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E) && !dialogueManager.isTextDisplaying)
                {
                    dialogueManager.StartDialogue(1);
                }
            }
            if (hit.collider.CompareTag("Door"))
            {
                showShortInteractText.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    doorScript = hit.transform.gameObject.GetComponent<DoorScript>();

                    doorScript.doorInteract();
                }
            }
        }
        else
        {
            showInteractText.enabled = false;
            showShortInteractText.enabled = false;
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
                    dialogueManager.StartDialogue(4);
                }
                if (index == 1)
                {
                    showerDone = true;
                    Debug.Log("Shower completed");
                    waterEffect.Stop();
                    dialogueManager.StartDialogue(3);
                }
                if (index == 2)
                {
                    eatDone = true;
                    Debug.Log("Eat completed");
                    dialogueManager.StartDialogue(2);
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