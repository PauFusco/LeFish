using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONDialogueReader : MonoBehaviour
{
    public TextAsset textJson;

    [System.Serializable]
    public class Dialogue
    {
        public GameObject objectName;
        public string text;
    }

    [System.Serializable]
    public class DialogueList
    {
        public Dialogue[] dialogue;
    }

    public DialogueList dialogueList = new DialogueList();

    // Start is called before the first frame update
    void Start()
    {
        //dialogueList = JsonUtility.FromJson<DialogueList>(textJson.text).dialogue;
    }
}
