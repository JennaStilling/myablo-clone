using UnityEngine;

public class EnemyCombat : CombatReceiver
{
    public override void Die()
    {
        base.Die();
        // We'll notify the AI when the CombatReceiver dies
        GetComponent<BasicAI>().TriggerDeath();
        // We'll grant the player experience as well

    }
}
