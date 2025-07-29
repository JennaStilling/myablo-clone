using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    List<Quest> activeQuests = new List<Quest>();

    public static QuestManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public Quest GetQuest(string questName)
    {
        Quest returnQuest = null;

        foreach(Quest quest in activeQuests)
        {
            if (quest.GetQuestName() == questName) returnQuest = quest;
        }

        return returnQuest;
    }

    public void ProgressQuestToNextStep(string questName)
    {
        foreach(Quest quest in activeQuests)
        {
            if (quest.GetQuestName() == questName) quest.ProgressToNextStep();
        }
    }

    public void AddQuest(string newQuestName, int newQuestStep)
    {
        if (GetQuest(newQuestName) == null) activeQuests.Add(new Quest(newQuestName, newQuestStep));
    }
    public void CompleteQuest(string questName)
    {
        activeQuests.Remove(GetQuest(questName));
    }
}
