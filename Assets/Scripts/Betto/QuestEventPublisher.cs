using UnityEngine;
using UnityEngine.Events;

public class QuestEventPublisher : MonoBehaviour
{
    public struct QuestInformation
    {
        public string questName;
        public QuestInformation(string questName)
        {
            this.questName = questName;
        }
    }

    [System.Serializable]
    public class QuestCompleteEvent : UnityEvent<QuestInformation>
    {

    }

    public QuestCompleteEvent questCompleteEvent;
}
