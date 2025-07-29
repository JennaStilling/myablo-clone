using UnityEngine;

public class CombatReceiver : Clickable
{
    protected int factionID = 0;

    [SerializeField] protected float maxHP = 35;
    protected float currentHP = 35;
    protected bool alive = true;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float amount)
    {
        if (!alive) return;

        currentHP -= amount;
        if (currentHP <= 0) Die();
    }

    public virtual void Die()
    {
        alive = false;
    }

    public virtual void Revive()
    {

    }

    public void SetFactionID(int newID)
    {
        factionID = newID;
    }
    public int GetFactionID()
    {
        return factionID;
    }
}
