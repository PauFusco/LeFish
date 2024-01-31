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
    public List<string> dialogueBuffer;
    public string currentDialogue;

    // Start is called before the first frame update
    private void Start()
    {
        soundManager = GetComponent<SoundManager>()
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (textComponent.text == currentDialogue)
            {
                textComponent.text = string.Empty;
                // start coroutine with next dialogue
                playNextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = currentDialogue;
            }
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
    /// Used to add a custom line of dialogue to array of dialogues to play
    /// </summary>
    /// <param name="line"></param>
    private void addDialogueToBuffer(string line)
    {
        dialogueBuffer.Add(line);
    }

    /// <summary>
    /// Used to add a predefined line of dialogue to array of dialogues to play
    /// </summary>
    /// <param name="index"></param>
    private void addDialogueToBuffer(int index)
    {
        dialogueBuffer.Add(dialogues[index]);
    }

    private void playNextLine()
    {
        dialogueBuffer.RemoveAt(0);

        gameObject.SetActive(true);
        StartCoroutine("TypeLine");
    }
}