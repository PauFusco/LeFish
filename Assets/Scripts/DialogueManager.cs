using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public string[] texts;

    public TextMeshProUGUI textComponent;

    public float textSpeed;

    public bool isTextDisplaying;
    // Start is called before the first frame update
    void Start()
    {
        isTextDisplaying = false;
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    if (textComponent.text == lines[index])
        //    {
        //        NextLine();
        //    }
        //}
    }

    public void StartDialogue(int index)
    {
        isTextDisplaying = true;
        StartCoroutine(TypeLine(index));
    }

    IEnumerator TypeLine(int index)
    {
        foreach (char c in texts[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(4);
        textComponent.text = string.Empty;
        isTextDisplaying = false;
    }

    //void NextLine()
    //{
    //    if (index < lines.Length - 1)
    //    {
    //        index++;
    //        textComponent.text = string.Empty;

    //    }
    //    else
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}
}
