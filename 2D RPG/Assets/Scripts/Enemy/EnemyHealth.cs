using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [Header("Config")]
    [SerializeField] private float health;
    
    public float CurrentHealth { get; private set; }

    private readonly int dead = Animator.StringToHash("Dead");
    
    private Animator animator;
    private EnemyBrain enemyBrain;
    private EnemySelector enemySelector;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        enemySelector = GetComponent<EnemySelector>();
    }

    private void Start()
    {
        CurrentHealth = health;
    }
    
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0f)
        {
            animator.SetTrigger(dead);
            enemyBrain.enabled = false;
            enemySelector.NoSelectedCallback();
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        else
        {
            DamageManager.Instance.ShowDamageText(amount, transform);
        }
    }
}
