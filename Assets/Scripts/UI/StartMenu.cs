using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Vector3 start = new Vector3(0, 1, -8.8f);
    [SerializeField] private Vector3 end = new Vector3(0, 1, -1.6f);
    [SerializeField] private float durationCamera = 3.0f;

    [SerializeField] private Image background;
    [SerializeField] private float durationFade = 0.5f;
    private Color startColor = new Color(0f, 0f, 0f, 0f); 
    private Color endColor = new Color(0f, 0f, 0f, 1f);

    private void Start()
    {
        background.gameObject.SetActive(false);
    }

    public void PlayGame()
    {
        StartCoroutine(MoveCameraAndLoadScene());
    }

    IEnumerator MoveCameraAndLoadScene()
    {
        float elapsedTime = 0f;

        while (elapsedTime < durationCamera)
        {
            Camera.main.transform.position = Vector3.Lerp(start, end, elapsedTime / durationCamera);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the camera reaches the final position exactly
        Camera.main.transform.position = end;

        elapsedTime = 0f;

        background.gameObject.SetActive(true);

        while (elapsedTime < durationFade)
        {
            background.color = Color.Lerp(startColor, endColor, elapsedTime / durationFade);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }


        // After moving the camera, load the scene
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
