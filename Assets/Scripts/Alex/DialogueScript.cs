using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public GameObject box;
    public Text title;
    public Text words;
    public Text button_text;

    private List<string> toSay;

    // Start is called before the first frame update
    void Start()
    {
        /*List<string> oof = new List<string>();
        oof.Add("Dolly and Dot are my best friends.");
        oof.Add("They pull my wagons through dunes of sand.");
        oof.Add("They have small teeth and they love to eat...");
        StartDialogue("Meerah", oof);*/
    }

    // Update is called once per frame
    void Update()
    {}

    public void StartDialogue(string speaker, List<string> sentences)
    {
        box.SetActive(true);
        SetTitle(speaker);
        words.text = sentences[0];
        sentences.RemoveAt(0);
        toSay = sentences;
        button_text.text = "Continue...";
    }

    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    public void ContinueDialogue()
    {
        if(toSay.Count > 0)
        {
            words.text = toSay[0];
            toSay.RemoveAt(0);
            if(toSay.Count == 0)
            {
                button_text.text = "End";
            }
        } else
        {
            box.SetActive(false);
        }
    }
}
