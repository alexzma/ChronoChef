using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Attached to NPCs
public class NpcBasic : MonoBehaviour
{
    public DialogueScript dialogueScript;
    public IngredientTracker ingredientTracker;
    public List<string> preQuestSentences;
    public List<string> postQuestSentences;
    public List<string> postQuestEndSentences;
    public string ingredientToGivePlayer;
    public string npcName;
    public string questToBeCompletedName;
    private bool isPreQuest;
    private bool doneTalkingLast;

    public void Awake()
    {
        isPreQuest = true;
        doneTalkingLast = false;
    }

    public void TalkToNpc()
    {
        Debug.Log("talking");
        if (isPreQuest) {
            dialogueScript.StartDialogue(npcName, CopyListString(preQuestSentences));
        } else
        {
            if (!doneTalkingLast)
            {
                dialogueScript.StartDialogue(npcName, CopyListString(postQuestSentences));
                doneTalkingLast = true;
            } else
            {
                dialogueScript.StartDialogue(npcName, CopyListString(postQuestEndSentences));
            }
        }
    }

    public void questComplete(QuestEventPublisher.QuestInformation questInfo)
    {
        if (questInfo.questName == questToBeCompletedName)
        {
            isPreQuest = false;
        }
    }

    private void GivePlayerItem()
    {
        ingredientTracker.VerifyIngredient(ingredientToGivePlayer);
    }

    private List<string> CopyListString(List<string> list)
    {
        return list.Select(item => (string)item.Clone()).ToList();
    }
}
