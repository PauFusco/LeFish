using System.Collections;
using UnityEngine;

public class FishTank : MonoBehaviour
{
    public ProgressBar progressBar;

    [SerializeField] private GameObject FishFood;
    [SerializeField] private GameObject Tank;

    [SerializeField] private Transform PlayerHand;
    [SerializeField] private LayerMask Layer;
    [SerializeField] private float InteractRange;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, Layer))
        {
            // Interact with objects using key E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Check if the hit object is the FishTank
                if (hitInfo.collider.CompareTag("FishTank"))
                {
                    // Trigger the animation on the FishFood object
                    if (FishFood != null && FishFood.transform.position == PlayerHand.position)
                    {
                        // Assuming you have an Animator component on the FishFood
                        Animator fishFoodAnimator = FishFood.GetComponent<Animator>();

                        if (fishFoodAnimator != null)
                        {
                            // Trigger the animation (assuming you have a trigger parameter named "Activate")
                            fishFoodAnimator.SetTrigger("Activate");
                        }
                    }
                }
            }
        }
    }
}
