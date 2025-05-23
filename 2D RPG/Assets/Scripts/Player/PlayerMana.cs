using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt)) UseMana(2f);
    }
    
    public void UseMana(float amount)
    {
        if (stats.Mana >= amount)
        {
            stats.Mana = Mathf.Max(stats.Mana - amount, 0f);
        }
    }
}