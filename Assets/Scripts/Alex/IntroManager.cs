using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public DialogueScript dialogueScript;
    public SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        List<string> sentences = new List<string>();
        sentences.Add("Hey, you! You're finally awake.");
        sentences.Add("There's a cooking competition today!");
        sentences.Add("You have to gather ingredients, and use it to cook the best ramen!");
        sentences.Add("Good luck, and have fun!");
        dialogueScript.StartDialogue("Chef", sentences);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueScript.box.activeSelf)
        {
            sceneLoader.LoadScene("Alex_Game_Scene");
        }
    }
}
