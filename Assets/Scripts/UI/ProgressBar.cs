using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image progressBar;

    float progress, maxProgress = 100;
    float lerpSpeed;
    
    // Start is called before the first frame update
    private void Start()
    {
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (progress > maxProgress)
        {
            progress = maxProgress;
        }

        lerpSpeed = 3f * Time.deltaTime;

        FillProgressBar();
        ColorChanger();
    }

    private void FillProgressBar()
    {
        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progress / maxProgress, lerpSpeed);

        if (progress < maxProgress)
        {
            progress += 0.1f;
        }
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (progress / maxProgress));

        progressBar.color = healthColor;
    }
}
