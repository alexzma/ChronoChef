using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Attached to NPCs
public class NpcBasic : MonoBehaviour
{
    private DialogueScript dialogueScript;
    public IngredientTracker ingredientTracker;
    public List<string> preQuestSentences;
    public List<string> postQuestSentences;
    public List<string> postQuestEndSentences;
    public string ingredientToGivePlayer;
    public GameObject ingredient;
    public string npcName;
    public string questToBeCompletedName;
    private bool isPreQuest;
    private bool doneTalkingLast;
    private Transform player;
    private Movement playerMovement;
    private QuestManager questManager;
    public bool endGame = false;
    public BombsManager bombManager;

    public void Awake()
    {
        isPreQuest = true;
        doneTalkingLast = false;
    }

    public void Start()
    {
        dialogueScript = GetComponent<DialogueScript>();
        dialogueScript.alertPlayer = true;
        dialogueScript.TurnOffBox();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("Found player transform at position " + player.position);
        playerMovement = player.GetComponent<Movement>();
        questManager = GameObject.FindObjectOfType<QuestManager>();
    }

    public void TalkToNpc()
    {
        if (isPreQuest && questManager.GetQuestResult(npcName))
        {
            isPreQuest = false;
            GivePlayerItem();
        }

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

    public void TalkToNpc(float sec)
    {
        Invoke("TalkToNpc", sec);
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
        //ingredientTracker.VerifyIngredient(ingredientToGivePlayer);
        Instantiate(ingredient, transform.position + Vector3.up, Quaternion.identity);
    }

    private List<string> CopyListString(List<string> list)
    {
        return list.Select(item => (string)item.Clone()).ToList();
    }

    public void SignalEndOfTalk()
    {
        if (endGame)
        {
            int score = Mathf.RoundToInt(ingredientTracker.CalculatePercentageDone() * 8);
            if (bombManager != null && score == 8)
            {
                if (bombManager.GetFuture() > 5)
                    score++;
                if (bombManager.GetPast() > 5)
                    score++;
            }
            PlayerPrefs.SetInt("Score", score);
            GetComponent<SceneLoader>().LoadScene("Master_End_Scene");
        }
        playerMovement.ReleaseFreeze();
    }
}
