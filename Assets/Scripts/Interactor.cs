using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask InteractableLayer;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private float ThrowingForce;
    [SerializeField] private float InteractRange;
    [SerializeField] private Transform PlayerHand;

    private Rigidbody CurrentObjectRigidBody;
    private Collider CurrentObjectCollider;

    // Update is called once per frame
    void Update()
    {
        // Interact with objects using key E
        if (Input.GetKeyDown(KeyCode.E)) {
            Ray r = new Ray(PlayerCamera.transform.position, PlayerCamera.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, InteractableLayer)) {
                // Replace
                if (CurrentObjectRigidBody)
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

            

            // Drop (without replacing)
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

        // Throw
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (CurrentObjectRigidBody)
            {
                CurrentObjectRigidBody.isKinematic = false;
                CurrentObjectCollider.enabled = true;

                CurrentObjectRigidBody.AddForce(PlayerCamera.transform.forward * ThrowingForce, ForceMode.Impulse);

                CurrentObjectRigidBody = null;
                CurrentObjectCollider = null;
            }
        }

        // Pick pt2 (and move to hand)
        if (CurrentObjectRigidBody)
        {
            // Move object to hand (hand is invisible, it looks like is near camera)
            CurrentObjectRigidBody.position = PlayerHand.position;
            CurrentObjectRigidBody.rotation = PlayerHand.rotation;
        }
    }
}
