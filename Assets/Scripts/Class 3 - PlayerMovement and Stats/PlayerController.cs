using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] ClassSkillManager skillManager;

    [SerializeField] EquippableAbility ability1;
    [SerializeField] EquippableAbility ability2;

    int factionID = 1;
    bool alive = true;

    bool inDialog = false;

    public static PlayerController instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera.main.gameObject.AddComponent<CameraController>();
        Camera.main.gameObject.GetComponent<CameraController>().SetFollowTarget(gameObject);
        EventsManager.instance.onDialogStarted.AddListener(StartDialogMode);
        EventsManager.instance.onDialogEnded.AddListener(EndDialogMode);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onDialogStarted.RemoveListener(StartDialogMode);
        EventsManager.instance.onDialogEnded.RemoveListener(EndDialogMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (inDialog) return;
        if (!alive) return;

        if (Input.GetMouseButtonDown(0) && ability1 != null) UseAbility1();
        if (Input.GetMouseButtonDown(1) && ability2 != null) UseAbility2();
    }

    void UseAbility1()
    {
        ability1.RunAbilityClicked(this);
    }
    void UseAbility2()
    {
        ability2.RunAbilityClicked(this);
    }
    public void SetAbility2(EquippableAbility newAbility)
    {
        ability2 = newAbility;
        EventsManager.instance.onNewAbility2Equipped.Invoke(ability2);
    }
    public EquippableAbility GetAbility2()
    {
        return ability2;
    }

    public PlayerMovement Movement()
    {
        return GetComponent<PlayerMovement>();
    }
    public PlayerAnimator GetAnimator()
    {
        return GetComponent<PlayerAnimator>();
    }
    public PlayerCharacterSheet GetCharacterSheet()
    {
        return GetComponent<PlayerCharacterSheet>();
    }
    public PlayerCombat Combat()
    {
        return GetComponent<PlayerCombat>();
    }
    public ClassSkillManager SkillManager()
    {
        return skillManager;
    }
    public int GetFactionID()
    {
        return factionID;
    }

    public void TriggerDeath()
    {
        alive = false;
        GetAnimator().TriggerDeath();
    }
    public void TriggerRevive()
    {
        alive = true;
        GetAnimator().TriggerRevive();
    }

    #region Dialog Listeners
    public void StartDialogMode()
    {
        inDialog = true;
    }
    public void EndDialogMode()
    {
        inDialog = false;
    }
    #endregion
}
