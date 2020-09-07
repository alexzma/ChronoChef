using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeInteractor : MonoBehaviour
{
    public string questName;
    public QuestEventPublisher publisher;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            QuestEventPublisher.QuestInformation questInfo = new QuestEventPublisher.QuestInformation(questName);
            publisher.questCompleteEvent.Invoke(questInfo);
        }
    }
}
