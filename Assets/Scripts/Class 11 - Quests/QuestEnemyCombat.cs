using UnityEngine;

public class QuestEnemyCombat : EnemyCombat
{
    [SerializeField] QuestStep questStep;
    public override void Die()
    {
        base.Die();
        if (questStep.QuestIsOnThisStep()) questStep.ProgressQuest();
    }
}
