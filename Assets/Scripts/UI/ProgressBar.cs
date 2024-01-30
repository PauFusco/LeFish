using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image progressBar;

    public float progress = 0, maxProgress = 100;
    float lerpSpeed;

    public void FillProgressBar()
    {
        progressBar.fillAmount = progress;

        lerpSpeed = 3f * Time.deltaTime;
        
        // Fill
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progress / maxProgress, lerpSpeed);
        progress += 0.1f;
        
        // Color
        Color healthColor = Color.Lerp(Color.red, Color.green, (progress / maxProgress));
        progressBar.color = healthColor;


        if (progress > maxProgress)
        {
            progress = maxProgress;
        }
    }

}
