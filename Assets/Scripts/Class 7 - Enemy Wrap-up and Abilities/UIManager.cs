using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playerHUD;
    [SerializeField] GameObject characterStatsPanel;
    [SerializeField] GameObject skillTree;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        // HideAll();
    }

    public void HideAll()
    {
        // Hide everything
        HidePlayerHUD();
        HideCharacterStatsPanel();
        HideSkillTree();
    }

    #region PlayerHUD
    public void ShowPlayerHUD()
    {
        playerHUD.SetActive(true);
    }
    public void HidePlayerHUD()
    {
        playerHUD.SetActive(false);
    }
    #endregion

    #region Character Stats Panel
    public void ShowCharacterStatsPanel()
    {
        characterStatsPanel.SetActive(true);
    }
    public void HideCharacterStatsPanel()
    {
        characterStatsPanel.SetActive(false);
    }
    public void ToggleCharacterStatsPanel()
    {
        if (characterStatsPanel.activeInHierarchy) HideCharacterStatsPanel();
        else ShowCharacterStatsPanel();
    }
    #endregion

    #region SkillTree
    public void ShowSkillTree()
    {
        skillTree.SetActive(true);
    }
    public void HideSkillTree()
    {
        skillTree.SetActive(false);
    }
    public void ToggleSkillTree()
    {
        if (skillTree.activeInHierarchy) HideSkillTree();
        else ShowSkillTree();
    }
    #endregion
}
