using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class FishTank : MonoBehaviour
{
    public ProgressBar progressBar;
    public VisualEffect food;

    [SerializeField] private GameObject FishFood;
    [SerializeField] private GameObject Tank;

    [SerializeField] private Transform PlayerHand;
    [SerializeField] private LayerMask ActionLayer;
    [SerializeField] private float InteractRange;

    public bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        progressBar.progress = 0;
        food.enabled = false;
        progressBar.progressBar.enabled = false;
        progressBar.progressBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, ActionLayer))
        {
            // Check if the hit object is the FishTank
            if (hitInfo.collider.CompareTag("FishTank")) 
            {
                // Interact with objects using key E
                if (Input.GetKey(KeyCode.E) && !isDone /*&& FishFood.transform.position == PlayerHand.position*/) //FishFood is dropped, need to check
                {
                    progressBar.progressBar.enabled = true;
                }
            }

            if (progressBar.enabled)
            {
                progressBar.FillProgressBar();

                if (progressBar.progress == progressBar.maxProgress)
                {
                    progressBar.progressBar.enabled = false;
                    isDone = true;
                }
            }
        }
    }
}
