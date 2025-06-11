using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private PlayerStats stats;
    
    [Header("Bars")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image manaBar;
    [SerializeField] private Image expBar;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;

    [Header("Stat Panel")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI statLevelTMP;
    [SerializeField] private TextMeshProUGUI statDamageTMP;
    [SerializeField] private TextMeshProUGUI statCChanceTMP;
    [SerializeField] private TextMeshProUGUI statCDamageTMP;
    [SerializeField] private TextMeshProUGUI statTotalExpTMP;
    [SerializeField] private TextMeshProUGUI statCurrentExpTMP;
    [SerializeField] private TextMeshProUGUI statRequiredExpTMP;
    
    private void Update()
    {
        UpdatePlayerUI();
    }

    public void OpenCloseStatsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
        if(statsPanel.activeSelf)
            UpdateStatsPanel();
    }
    
    private void UpdatePlayerUI()
    {
        levelTMP.text = $"Level {stats.Level}";
        healthTMP.text = $"{stats.Health} / {stats.MaxHealth}";
        manaTMP.text = $"{stats.Mana} / {stats.MaxMana}";
        expTMP.text = $"{stats.CurrentExp} / {stats.NextLevelExp}";

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.Health/stats.MaxHealth, 10f * Time.deltaTime);
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, stats.Mana/stats.MaxMana, 10f * Time.deltaTime);
        expBar.fillAmount = Mathf.Lerp(expBar.fillAmount, stats.CurrentExp/stats.NextLevelExp, 10f * Time.deltaTime);
    }

    private void UpdateStatsPanel()
    {
        statLevelTMP.text = stats.Level.ToString();
        statDamageTMP.text = stats.TotalDamage.ToString();
        statCChanceTMP.text = stats.CriticalChance.ToString();
        statCDamageTMP.text = stats.CriticalDamage.ToString();
        statTotalExpTMP.text = stats.TotalExp.ToString();
        statCurrentExpTMP.text = stats.CurrentExp.ToString();
        statRequiredExpTMP.text = stats.NextLevelExp.ToString();
    }
}
