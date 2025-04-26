using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) TakeDamage(1f);
    }

    public void TakeDamage(float amount)
    {
        stats.Health -= amount;
        if(stats.Health <= 0) PlayerDeath();
    }

    private void PlayerDeath()
    {
        Debug.Log("Player died");
    }
}
