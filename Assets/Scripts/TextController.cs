using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    public GameObject soundManagerObj;
    private SoundManager soundManager;

    public List<string> dialogues;

    public string currentDialogue;

    // Start is called before the first frame update
    private void Start()
    {
        soundManager = soundManagerObj.GetComponent<SoundManager>();
        dialogues.Add("Hola Criaturitas");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            textComponent.text = string.Empty;
            playDialogue(0);
        }
        if (currentDialogue == string.Empty)
        {
            StopAllCoroutines();
            textComponent.text = string.Empty;
        }
    }

    private IEnumerator TypeLine()
    {
        foreach (char c in currentDialogue.ToCharArray())
        {
            textComponent.text += c;
            soundManager.SelectAudio(3, 1f);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    /// <summary>
    /// Index 0 Debug Dialogue
    /// </summary>
    /// <param name="index"></param>
    private void playDialogue(int index)
    {
        currentDialogue = dialogues[index];
        StartCoroutine("TypeLine");
    }
}