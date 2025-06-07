using System;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    public float CurrentMana { get; private set; }

    private void Start()
    {
        ResetMana();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt)) UseMana(2f);
    }
    
    public void UseMana(float amount)
    {
        stats.Mana = Mathf.Max(stats.Mana - amount, 0f);
        CurrentMana = stats.Mana;
    }

    public void ResetMana()
    {
        CurrentMana = stats.Mana;
    }
}