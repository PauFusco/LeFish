using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask ItemLayer;
    [SerializeField] private LayerMask ActionLayer;
    [SerializeField] private float ThrowingForce;
    [SerializeField] private float InteractRange;
    [SerializeField] private Transform PlayerHand;

    private Rigidbody CurrentObjectRigidBody;
    private Collider CurrentObjectCollider;

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
            
            }
            if (hitInfo.collider.CompareTag("Shower"))
            {
            
            } 
            if (hitInfo.collider.CompareTag("Food"))
            {
            
            }
            if (hitInfo.collider.CompareTag("Bed"))
            {
            
            }
            if (hitInfo.collider.CompareTag("Door"))
            {
            
            }
            
        }

    }
}
