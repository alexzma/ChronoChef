using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Attached to NPCs
public class NpcBasic : MonoBehaviour
{
    public DialogueScript dialogueScript;
    public List<string> preQuestSentences;
    public List<string> postQuestSentences;
    public string npcName;
    public string questToBeCompletedName;
    private bool isPreQuest;

    public void Awake()
    {
        isPreQuest = true;
    }

    public void TalkToNpc()
    {
        Debug.Log("talking");
        if (isPreQuest) {
            dialogueScript.StartDialogue(npcName, CopyListString(preQuestSentences));
        } else
        {
            dialogueScript.StartDialogue(npcName, CopyListString(postQuestSentences));
        }
    }

    public void questComplete(QuestEventPublisher.QuestInformation questInfo)
    {
        if (questInfo.questName == questToBeCompletedName)
        {
            isPreQuest = false;
        }
    }

    public List<string> CopyListString(List<string> list)
    {
        return list.Select(item => (string)item.Clone()).ToList();
    }
}
