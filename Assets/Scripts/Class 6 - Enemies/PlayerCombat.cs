using UnityEngine;

public class PlayerCombat : CombatReceiver
{
    protected float currentMana = 35;
    [SerializeField] protected float maxMana = 35;

    // Regen Variables
    protected float healthRegenBase = 0.5f;
    protected float healthRegenMod = 1f;
    protected float manaRegenBase = 0.5f;
    protected float manaRegenMod = 1f;
    protected float regenUpdateTickTimer = 0f;
    protected float regenUpdateTickTime = 2f;

    protected virtual void Update()
    {
        RunRegen();
    }

    #region Overrides
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        SetFactionID(GetComponent<PlayerController>().GetFactionID());
        EventsManager.instance.onPlayerLeveledUp.AddListener(LevelUp);
        EventsManager.instance.onStatPointSpent.AddListener(StatsChangedAdjustment);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(LevelUp);
        EventsManager.instance.onStatPointSpent.RemoveListener(StatsChangedAdjustment);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        EventsManager.instance.onHealthChanged.Invoke(currentHP/maxHP);
    }

    public override void Die()
    {
        base.Die();
        GetComponent<PlayerController>().TriggerDeath();
    }
    #endregion

    #region Mana Management
    public float GetMana()
    {
        return currentMana;
    }
    public void SpendMana(float amount)
    {
        currentMana -= amount;
        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
    }
    #endregion

    #region Level Up Events
    void LevelUp()
    {
        currentHP = maxHP;
        currentMana = maxMana;

        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }

    void StatsChangedAdjustment()
    {
        UpdateBaseRegen();

        float oldMaxHP = maxHP;
        float oldMaxMana = maxMana;

        maxHP = PlayerCharacterSheet.instance.GetMaxHP();
        maxMana = PlayerCharacterSheet.instance.GetMaxMana();

        currentHP += maxHP - oldMaxHP;
        currentMana += maxMana - oldMaxMana;
        EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
    }
    #endregion

    #region Regen
    protected void RunRegen()
    {
        currentHP += (Time.deltaTime * healthRegenBase * healthRegenMod);
        if (currentHP > maxHP) currentHP = maxHP;

        currentMana += (Time.deltaTime * manaRegenBase * manaRegenMod);
        if (currentMana > maxMana) currentMana = maxHP;

        regenUpdateTickTimer += Time.deltaTime;
        if(regenUpdateTickTimer >= regenUpdateTickTime)
        {
            regenUpdateTickTimer -= regenUpdateTickTime;
            EventsManager.instance.onHealthChanged.Invoke(currentHP / maxHP);
            EventsManager.instance.onManaChanged.Invoke(currentMana / maxMana);
        }
    }
    
    public void SetHPRegenMod(float newMod)
    {
        healthRegenMod = newMod;
    }
    public void SetManaRegenMod(float newMod)
    {
        manaRegenMod = newMod;
    }

    protected void UpdateBaseRegen()
    {
        healthRegenBase = .5f + (.01f * PlayerCharacterSheet.instance.GetVitality());
        manaRegenBase = .5f + (.01f * PlayerCharacterSheet.instance.GetEnergy());
    }
    #endregion
}
