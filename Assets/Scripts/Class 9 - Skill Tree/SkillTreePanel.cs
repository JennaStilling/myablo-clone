using UnityEngine;
using System.Collections.Generic;

public class SkillTreePanel : MonoBehaviour
{
    List<ClassSkill> skillTree = new List<ClassSkill>();

    [SerializeField] List<SkillTreeButton> skillTreeButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerController.instance != null) skillTree = PlayerController.instance.SkillManager().GetSkillTree();
        HideSkillTree();
        EventsManager.instance.onSkillPointSpent.AddListener(UpdateSkillTree);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onSkillPointSpent.RemoveListener(UpdateSkillTree);
    }


    // Update is called once per frame
    void OnEnable()
    {
        if (skillTree != null) UpdateSkillTree();
        else
        {
            skillTree = PlayerController.instance.SkillManager().GetSkillTree();
            UpdateSkillTree();
        }
    }

    void UpdateSkillTree()
    {

        int i = 0;
        foreach(SkillTreeButton button in skillTreeButtons)
        {
            if (button != null)
            {
                if (i < skillTree.Count && skillTree[i] != null)
                {
                    button.gameObject.SetActive(true);
                    button.UpdateButton(skillTree[i]);
                }
                else
                {
                    button.gameObject.SetActive(false);
                }
            }
            i++;
            if (i >= skillTreeButtons.Count)
            {
                Debug.LogWarning("More skills than button slots in the active Class Skill Manager");
                i--;
            }
        }
    }

    public void HideSkillTree()
    {
        if(UIManager.instance != null) UIManager.instance.HideSkillTree();
    }
}
