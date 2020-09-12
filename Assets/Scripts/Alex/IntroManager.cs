using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public DialogueScript dialogueScript;
    public SceneLoader sceneLoader;
    public string next_scene;

    bool instructions_loaded = false;

    // Start is called before the first frame update
    void Start()
    {
        List<string> sentences = new List<string>();
        sentences.Add("Long ago, in a distant island, known as the land of the rising sun, there lived a humble broth maker.");
        sentences.Add("Dedicated to the culinary craft of broth making, the man sought to create the ultimate recipe...");
        sentences.Add("...a masterpiece extending numerous generations, a meal people would forever remember and enjoy - especially poor with no food.");
        sentences.Add("Unfortunately, a lack of ingredients hindered his bowl of dreams.");
        sentences.Add("The barren island could’t support a multitude of livestock, plants, and spices.");
        sentences.Add("UNTIL… the man discovered an anomaly on his island - a time anomaly.");
        sentences.Add("Utilizing the powers of science, he reworked the 4th dimension in order to achieve his real goal - soup cooking.");
        sentences.Add("Equipped with his chronobombs, the chef manipulated time to obtain his ingredients, becoming a CHRONO CHEF.");
        dialogueScript.StartDialogue("Introduction", sentences);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueScript.box.activeSelf)
        {
            if (instructions_loaded)
            {
                //sceneLoader.LoadScene("Alex_Game_Scene");
                Debug.Log("Instructions loaded");
            } else
            {
                List<string> sentences = new List<string>();
                sentences.Add("WASD moves the player while the arrow keys turn him.");
                sentences.Add("Q cycles through your chronobombs.");
                sentences.Add("Chronobombs change the time state of items, moving them into the future or the past.");
                sentences.Add("SPACE inspects, lifts, and places items and talks to people.");
                sentences.Add("Return here once you have all the ingredients to make THE ULTIMATE BROTH.");
                dialogueScript.StartDialogue("Instructions", sentences);
                instructions_loaded = true;
            }
        }
        if (!dialogueScript.box.activeSelf && instructions_loaded)
        {
            sceneLoader.LoadScene(next_scene);
        }
    }
}
