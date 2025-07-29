using UnityEngine;
using System;

[Serializable]
public class Quest
{
    [SerializeField] string questName = "";
    int currentQuestStep = 0;

    public Quest(string newQuestName, int currentQuestStep)
    {
        this.questName = newQuestName;
        this.currentQuestStep = currentQuestStep;
    }

    public string GetQuestName()
    {
        return questName;
    }

    public int GetCurrentStep()
    {
        return currentQuestStep;
    }

    public virtual void ProgressToNextStep()
    {
        currentQuestStep++;
        EventsManager.instance.onQuestStatusChanged.Invoke();
    }
}
