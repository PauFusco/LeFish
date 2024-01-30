using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressBar;

    public float progress, maxProgress = 100;
    float lerpSpeed;
    
    // Start is called before the first frame update
    private void Start()
    {
        progress = 0;
    }

    private void FillProgressBar()
    {
        while (progress < maxProgress)
        {
            lerpSpeed = 3f * Time.deltaTime;

            // Fill
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progress / maxProgress, lerpSpeed);
            progress += 0.1f;

            // Color
            Color healthColor = Color.Lerp(Color.red, Color.green, (progress / maxProgress));
            progressBar.color = healthColor;
        }

        if (progress > maxProgress)
        {
            progress = maxProgress;
        }
    }
}
