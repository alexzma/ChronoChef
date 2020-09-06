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
        toSay = new List<string>();
        /*List<string> oof = new List<string>();
        oof.Add("Dolly and Dot are my best friends.");
        oof.Add("They pull my wagons through dunes of sand.");
        oof.Add("They have small teeth and they love to eat...");
        StartDialogue("Meerah", oof);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ContinueDialogue();
        }
    }

    public void StartDialogue(string speaker, List<string> sentences)
    {
        box.SetActive(true);
        SetTitle(speaker);
        TypeSentence(sentences[0]);
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
            //words.text = toSay[0];
            StopAllCoroutines();
            StartCoroutine(TypeSentence(toSay[0]));
            toSay.RemoveAt(0);
            if(toSay.Count == 0)
            {
                button_text.text = "End";
            }
        } else
        {
            StopAllCoroutines();
            box.SetActive(false);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        words.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            words.text += letter;
            yield return null;
            yield return null;
            yield return null;
        }
    }
}
