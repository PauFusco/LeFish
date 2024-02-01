using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public enum StatusState { START, SUBTLE1, DEEP, SUBTLE2, REALITY };
    public enum FishState { FEED1, FEED2, WTF };
    public enum BedState { NOSLEEP, NIGHT1, NIGHT2, NIGHT3 };
    public enum FoodState { BFEAT1, EAT1, FISHMEAT };
    public enum ShowerState { OTAKU1, OTAKU2, OTAKU3 };

    public string[] statusTexts;
    public string[] fishTexts;
    public string[] bedTexts;
    public string[] foodTexts;
    public string[] showerTexts;

    [HideInInspector]
    public StatusState status;
    [HideInInspector]
    public FishState fish;
    [HideInInspector]
    public BedState bed;
    [HideInInspector]
    public FoodState food;
    [HideInInspector]
    public ShowerState shower;

    public TextMeshProUGUI textComponent;

    public float textSpeed;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
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

    //void StartDialogue()
    //{
    //    index = 0;
    //    StartCoroutine(TypeLine());
    //}

    //IEnumerator TypeLine()
    //{
    //    foreach (char c in lines[index].ToCharArray())
    //    {
    //        textComponent.text += c;
    //        yield return new WaitForSeconds(textSpeed);
    //    }
    //}

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
