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

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectsWithTag("Respawn")[0].GetComponent<PlayerMovement>();
        soundManager = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

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
                if
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
                GameObject doorParent = hitInfo.collider.transform.root.gameObject;

                //An Animator variable is created for the doorParent's Animator component
                Animator doorAnim = doorParent.GetComponent<Animator>();

                //An AudioSource variable is created for the door's Audio Source component
                AudioSource doorSound = hitInfo.collider.gameObject.GetComponent<AudioSource>();

                //The interaction text is set active
                //intText.SetActive(true);

                //If the E key is pressed
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ////If the door's Animator's state is set to the open animation
                    //if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
                    //{
                    //    //The door's open animation trigger is reset
                    //    doorAnim.ResetTrigger("open");
                    //
                    //    //The door's close animation trigger is set (it plays)
                    //    doorAnim.SetTrigger("close");
                    //}
                    ////If the door's Animator's state is set to the close animation
                    //if (doorAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
                    //{
                    //    //The door's close animation trigger is reset
                    //    doorAnim.ResetTrigger("close");
                    //
                    //    //The door's open animation trigger is set (it plays)
                    //    doorAnim.SetTrigger("open");
                    //}

                }
            }
        }

    }
}
