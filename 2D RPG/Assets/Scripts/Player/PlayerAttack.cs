using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform[] attackPositions;
    [Header("Melee Config")] 
    [SerializeField] private ParticleSystem slashFX;
    [SerializeField] private float minDistanceMeleeAttack;

    public Weapon CurrentWeapon { get; set; }
    private PlayerActions actions;
    private PlayerAnimations playerAnimations;
    private PlayerMovement playerMovement;
    private PlayerMana playerMana;
    private EnemyBrain enemyTarget;
    private Coroutine attackCoroutine;
    private Transform currentAttackPosition;
    private float currentAttackRotation;
    private float attackCooldownTimer = 0.75f;
    private float lastPlayerAttackTime = 0f;
    
    private void Awake()
    {
        actions = new PlayerActions();
        playerMana = GetComponent<PlayerMana>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Start()
    {
        CurrentWeapon = initialWeapon;
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        GetFirePosition();
        lastPlayerAttackTime += Time.deltaTime;
    }
    
    private void Attack()
    {
        if(enemyTarget == null  || !(lastPlayerAttackTime - attackCooldownTimer > 0)) 
            return;
        if (attackCoroutine != null) 
            StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(IEAttack());
        lastPlayerAttackTime = 0;
    }

    private IEnumerator IEAttack()
    {
        if (currentAttackPosition == null) yield break;
        if (CurrentWeapon.WeaponType == WeaponType.Magic)
        {
            if (playerMana.CurrentMana < CurrentWeapon.RequiredMana) yield break;
            MagicAttack();
        }
        else
        {
            MeleeAttack();
        }
        playerAnimations.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackAnimation(false);
    }

    private void MagicAttack()
    {
        Quaternion rotation = 
            Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
        Projectile projectile = Instantiate(CurrentWeapon.ProjectilePrefab, 
            currentAttackPosition.position, rotation);
        projectile.direction = Vector3.up;
        projectile.Damage = GetAttackDamage();
        playerMana.UseMana(CurrentWeapon.RequiredMana);
    }

    private void MeleeAttack()
    {
        slashFX.transform.position = currentAttackPosition.position;
        slashFX.Play();
        float currentDistanceToEnemy = 
            Vector3.Distance(enemyTarget.transform.position, transform.position);
        if (currentDistanceToEnemy <= minDistanceMeleeAttack)
            enemyTarget.GetComponent<IDamagable>().TakeDamage(GetAttackDamage());
    }

    private float GetAttackDamage()
    {
        float damage = stats.BaseDamage;
        damage += CurrentWeapon.Damage;
        float randomPerc = UnityEngine.Random.Range(0f, 100f);
        if (randomPerc <= stats.CriticalChance)
            damage += damage * (stats.CriticalDamage/100f);
        
        return damage;
    }
    
    private void GetFirePosition()
    {
        Vector2 moveDirection = playerMovement.MoveDirection;
        switch (moveDirection.x)
        {
            case > 0f:
                //attackPositions[1] = Player child game object Attack Right
                currentAttackPosition = attackPositions[1];
                // Z rotation for projectile
                currentAttackRotation = -90f;
                break;
            case < 0f:
                //Player child game object Attack Left
                currentAttackPosition = attackPositions[3];
                currentAttackRotation = -270f;
                break;
        }
        switch (moveDirection.y)
        {
            case > 0f:
                //Player child game object Attack Up
                currentAttackPosition = attackPositions[0];
                // Z rotation for projectile
                currentAttackRotation = 0f;
                break;
            case < 0f:
                //Player child game object Attack Left
                currentAttackPosition = attackPositions[2];
                currentAttackRotation = 180f;
                break;
        }
    }

    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        enemyTarget = enemySelected;
    }

    private void NoEnemySelectedCallback()
    {
        enemyTarget = null;
    }
    
    private void OnEnable()
    {
        actions.Enable();
        SelectionManager.OnEnemySelected += EnemySelectedCallback;
        SelectionManager.OnNoSelection += NoEnemySelectedCallback;
        EnemyHealth.OnEnemyDeadEvent += NoEnemySelectedCallback;
    }

    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelected -= EnemySelectedCallback;
        SelectionManager.OnNoSelection -= NoEnemySelectedCallback;
        EnemyHealth.OnEnemyDeadEvent -= NoEnemySelectedCallback;
    }
}