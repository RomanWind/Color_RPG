using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")] 
    [SerializeField] private Weapon initialWeapon;
    [SerializeField] private Transform[] attackPositions;
    
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
        actions.Attack.ClickAttack.performed += ctx => Attack();
    }

    private void Update()
    {
        GetFirePosition();
        lastPlayerAttackTime += Time.deltaTime;
    }
    
    private void Attack()
    {
        if(enemyTarget == null  ||
           !(lastPlayerAttackTime - attackCooldownTimer > 0)) 
            return;
        
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        attackCoroutine = StartCoroutine(IEAttack());
        lastPlayerAttackTime = 0;
    }

    private IEnumerator IEAttack()
    {
        if (currentAttackPosition != null)
        {
            if (playerMana.CurrentMana < initialWeapon.RequiredMana)
                yield break;
            
            Quaternion rotation = 
                Quaternion.Euler(new Vector3(0f, 0f, currentAttackRotation));
            Projectile projectile = Instantiate(initialWeapon.ProjectilePrefab, 
                currentAttackPosition.position, rotation);
            projectile.direction = Vector3.up;
            projectile.Damage = initialWeapon.Damage;
            playerMana.UseMana(initialWeapon.RequiredMana);
        }
        
        playerAnimations.SetAttackAnimation(true);
        yield return new WaitForSeconds(0.5f);
        playerAnimations.SetAttackAnimation(false);
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
    }

    private void OnDisable()
    {
        actions.Disable();
        SelectionManager.OnEnemySelected -= EnemySelectedCallback;
        SelectionManager.OnNoSelection -= NoEnemySelectedCallback;
    }
}