using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    private Player player;
    private PlayerAnimations playerAnimations;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }
    
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space)) TakeDamage(1f);
    }

    public void TakeDamage(float amount)
    {
        player.Stats.Health -= amount;
        if (player.Stats.Health <= 0f)
        {
            player.Stats.Health = 0f;
            PlayerDeath();
            return;
        }
        DamageManager.Instance.ShowDamageText(amount, transform);
    }

    private void PlayerDeath()
    {
        playerAnimations.SetDeadAnimation();
    }
}
