using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEngine.Timeline.TimelineAsset;
using Unity.VisualScripting;

using NUnit;


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
    [SerializeField] private LayerMask ItemLayer;

    // Range
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

    // Item holding
    private Rigidbody CurrentObjectRigidBody;
    private Collider CurrentObjectCollider;
    [SerializeField] private Transform PlayerHand;
    [SerializeField] private float ThrowingForce;

    [SerializeField] private Image background;
    [SerializeField] private float durationFade = 0.5f;
    private Color startColor = new Color(0f, 0f, 0f, 0f);
    private Color endColor = new Color(0f, 0f, 0f, 1f);

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

        background.gameObject.SetActive(false);
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
                        if(SceneManager.GetActiveScene().buildIndex == 4)
                        {
                            StartCoroutine(FadeOut());
                            SceneManager.LoadScene(0);
                            StartCoroutine(FadeIn());
                        }
                        else
                        {
                            StartCoroutine(FadeOut());
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                            StartCoroutine(FadeIn());
                        }
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
        if (Physics.Raycast(rayCast, out RaycastHit hitInfo, InteractRange, ItemLayer))
        {
            // Interact with objects using key E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Replace
                if (CurrentObjectRigidBody != null)
                {
                    // Reset physics of the object
                    CurrentObjectRigidBody.isKinematic = false;
                    CurrentObjectCollider.enabled = true;

                    // Replace it with new object
                    CurrentObjectRigidBody = hitInfo.rigidbody;
                    CurrentObjectCollider = hitInfo.collider;

                    // Disable physics for the object we are holding
                    CurrentObjectRigidBody.isKinematic = true;
                    CurrentObjectCollider.enabled = false;
                }
                // Pick pt1
                else
                {
                    // Disable physics for the object we are holding
                    CurrentObjectRigidBody = hitInfo.rigidbody;
                    CurrentObjectCollider = hitInfo.collider;

                    CurrentObjectRigidBody.isKinematic = true;
                    CurrentObjectCollider.enabled = false;
                }

                return;
            }
        }
        else
        {
            // Drop (without replacing)
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CurrentObjectRigidBody)
                {
                    // Reset physics of the object
                    CurrentObjectRigidBody.isKinematic = false;
                    CurrentObjectCollider.enabled = true;

                    // Set CurrentObject to null 
                    CurrentObjectRigidBody = null;
                    CurrentObjectCollider = null;
                }
            }
        }

        // Pick pt2 (and move to hand)
        if (CurrentObjectRigidBody)
        {
            // Move object to hand (hand is invisible, it looks like is near camera)
            CurrentObjectRigidBody.position = PlayerHand.position;
            CurrentObjectRigidBody.rotation = PlayerHand.rotation;
        }

        // Throw
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CurrentObjectRigidBody)
            {
                CurrentObjectRigidBody.isKinematic = false;
                CurrentObjectCollider.enabled = true;

                CurrentObjectRigidBody.AddForce(Camera.main.transform.forward * ThrowingForce, ForceMode.Impulse);

                CurrentObjectRigidBody = null;
                CurrentObjectCollider = null;
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

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        background.gameObject.SetActive(true);

        while (elapsedTime < durationFade)
        {
            background.color = Color.Lerp(endColor, startColor, elapsedTime / durationFade);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        background.gameObject.SetActive(true);

        while (elapsedTime < durationFade)
        {
            background.color = Color.Lerp(startColor, endColor, elapsedTime / durationFade);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

    }
}

