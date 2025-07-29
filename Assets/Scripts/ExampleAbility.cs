using UnityEngine;

public class ExampleAbility : ClassSkill
{
    public override void LevelUp()
    {
        if (PlayerCharacterSheet.instance.SkillPointSpendSuccessful())
        {
            skillLevel++;

            float healthRegenMod = 1 + .5f * skillLevel;
            PlayerController.instance.Combat().SetHPRegenMod(healthRegenMod);
        }
    }
}
