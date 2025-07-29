using UnityEngine;
using System;
using System.Collections.Generic;

public class ClassSkillManager : MonoBehaviour
{
    [SerializeField] ClassSkill basicMelee;
    [SerializeField] List<ClassSkill> skillTree = new List<ClassSkill>(); 

    public List<ClassSkill> GetSkillTree()
    {
        return skillTree;
    }
}