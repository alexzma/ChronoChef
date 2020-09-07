using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEventPublisher : MonoBehaviour
{
    public struct QuestInformation
    {
        public int questId;
        public QuestInformation(int questId)
        {
            this.questId = questId;
        }
    }
}
