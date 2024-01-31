using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskController : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    private InteractorRework interactorRework;

    // Start is called before the first frame update
    private void Start()
    {
        interactorRework = GetComponent<InteractorRework>();
    }

    // Update is called once per frame
    private void Update()
    {
        string textToPrint = "Tasks:\n";

        if (interactorRework.todolist.Count > 0)
        {
            if (interactorRework.todolist.Contains(InteractorRework.Tasks.FEED_FISH))
            {
                textToPrint += "Feed fish\n";
            }
            if (interactorRework.todolist.Contains(InteractorRework.Tasks.EAT))
            {
                textToPrint += "Eat\n";
            }
            if (interactorRework.todolist.Contains(InteractorRework.Tasks.SHOWER))
            {
                textToPrint += "Shower\n";
            }
        }
        else
        {
            Debug.Log("go to bed");
            textToPrint += "Go To Bed";
        }

        textComponent.text = textToPrint;
    }
}