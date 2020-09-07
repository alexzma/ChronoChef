using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    public DialogueScript dialogueScript;
    public SceneLoader sceneLoader;

    public int score;
    public int threshold;

    // Start is called before the first frame update
    void Start()
    {
        List<string> sentences = new List<string>();
        sentences.Add("Welcome back!");
        sentences.Add("Let's see what you made with your ingredients!");
        sentences.Add("Hmm...");
        sentences.Add("I give it a score of " + score.ToString() + ".");
        if(score > threshold)
        {
            sentences.Add("This is higher than the highest score of " + threshold.ToString() + ". Congratulations!");
        } else if(score == threshold)
        {
            sentences.Add("This is tied with the highest score of " + threshold.ToString() + ". Good enough, I suppose.");
        } else
        {
            sentences.Add("This is lower than the highest score of " + threshold.ToString() + ". Sorry for your loss.");
        }
        sentences.Add("Feel free to come back to the next competition!");
        dialogueScript.StartDialogue("Chef", sentences);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueScript.box.activeSelf)
        {
            sceneLoader.LoadScene("Peter_credits");
        }
    }
}
