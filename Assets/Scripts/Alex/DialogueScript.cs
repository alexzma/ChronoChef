using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public GameObject boxOutline;
    public GameObject box;
    public Text title;
    public Text words;
    public Text button_text;

    [HideInInspector]
    public bool alertPlayer = false;

    private List<string> toSay;
    private bool mute = true;

    public UnityEvent questCompleteEvent;

    // Start is called before the first frame update
    void Start()
    {
        toSay = new List<string>();
        /*List<string> oof = new List<string>();
        oof.Add("Dolly and Dot are my best friends.");
        oof.Add("They pull my wagons through dunes of sand.");
        oof.Add("They have small teeth and they love to eat...");
        StartDialogue("Meerah", oof);*/
        //List<string> sent = new List<string>();
        //sent.Add("Hello");
        //sent.Add("World");
        //sent.Add("No");
        //StartDialogue("Banana", sent);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (!mute)
                ContinueDialogue();
        }
    }

    public void StartDialogue(string speaker, List<string> sentences)
    {
        Debug.Log(sentences.Count);
        if (alertPlayer)
            boxOutline.SetActive(true);
        else
            box.SetActive(true);
        SetTitle(speaker);
        //TypeSentence(sentences[0]);
        //sentences.RemoveAt(0);
        toSay = sentences;
        button_text.text = "Continue...";
        ContinueDialogue();
        mute = false;
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
            if (alertPlayer)
            {
                boxOutline.SetActive(false);
                Invoke("AlertEndOfTalk", 0.3f);
            }
            else
                box.SetActive(false);
            questCompleteEvent.Invoke();
            mute = true;
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
        }
    }

    private void AlertEndOfTalk()
    {
        transform.GetComponent<NpcBasic>().SignalEndOfTalk();
    }

    public void TurnOffBox()
    {
        if (boxOutline.activeInHierarchy)
            boxOutline.SetActive(false);
    }
}
