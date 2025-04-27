using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    private PlayerAnimations animations;
    public PlayerStats Stats => stats;

    private void Awake()
    {
        animations = GetComponent<PlayerAnimations>();
    }
    
    public void ResetPlayer()
    {
        stats.ResetPlayer();
        animations.ResetPlayer();
    }
}
