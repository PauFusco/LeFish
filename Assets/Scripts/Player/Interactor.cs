using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Interact E
public class Interactor : MonoBehaviour
{
    // Player Movement
    private PlayerMovement playerMovement;

    // Sound Manager
    private SoundManager soundManager;

    // Interact
    [SerializeField] private LayerMask ItemLayer;
    [SerializeField] private LayerMask ActionLayer;
    [SerializeField] private float ThrowingForce;
    [SerializeField] private float InteractRange;
    [SerializeField] private Transform PlayerHand;

    // Item holding
    private Rigidbody CurrentObjectRigidBody;
    private Collider CurrentObjectCollider;

    // TASKS
    //Animator door;
    private bool doorOpen = false;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
        soundManager = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Esto da error
        Ray r = new Ray(transform.position, transform.forward);

        ItemInteract(r);
        ActionInteract(r);
    }

    void ItemInteract(Ray r)
    {
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, ItemLayer))
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
    }

    void ActionInteract(Ray r)
    {
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, ActionLayer))
        {
            if (hitInfo.collider.CompareTag("FishTank"))
            {
                // Key REPEAT
                playerMovement.canMove = false;
                //if
            }
            if (hitInfo.collider.CompareTag("Shower"))
            {
                // Key REPEAT
                playerMovement.canMove = false;

            }
            if (hitInfo.collider.CompareTag("Food"))
            {
                // Key REPEAT
                playerMovement.canMove = false;

            }
            if (hitInfo.collider.CompareTag("Bed"))
            {
                // Key DOWN
                playerMovement.canMove = false;

            }
            if (hitInfo.collider.CompareTag("Door"))
            {
                // Key DOWN

                //A GameObject variable is created for the door's main parent object
                GameObject doorParent = hitInfo.collider.gameObject;

                //An Animator variable is created for the doorParent's Animator component
                Animator doorAnim = doorParent.GetComponentInChildren<Animator>();

                //An AudioSource variable is created for the door's Audio Source component
                AudioSource doorSound = hitInfo.collider.gameObject.GetComponent<AudioSource>();

                //The interaction text is set active
                //intText.SetActive(true);

                //If the E key is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Objetc name: " + doorParent.name.ToString());
                    if (!doorOpen)
                    {
                        doorAnim.Play("DoorOpen", 0, 0.0f);
                        doorOpen = true;
                    }
                    else 
                    {
                        doorAnim.Play("DoorClose", 0, 0.0f);
                        doorOpen = false;
                    }
                }
            }
        }

    }
}
