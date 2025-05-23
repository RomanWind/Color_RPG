using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private PlayerAnimations playerAnimations;
    private float speed = 5f;
    private PlayerActions actions;
    private Rigidbody2D rb2D;
    private Vector2 moveDirection;

    private void Awake()
    {
        player = GetComponent<Player>();
        actions = new PlayerActions();
        rb2D = GetComponent<Rigidbody2D>();
        playerAnimations = GetComponent<PlayerAnimations>();
    }

    private void Update()
    {
        ReadMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(player.Stats.Health <= 0) return;
        
        rb2D.MovePosition(rb2D.position +  moveDirection * (speed * Time.fixedDeltaTime)); 
    }

    private void ReadMovement()
    {
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized;
        if (moveDirection == Vector2.zero)
        {
            playerAnimations.SetMoveBoolTransition(false);
            return;
        }
        playerAnimations.SetMoveBoolTransition(true);
        playerAnimations.SetMoveAnimation(moveDirection);
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
