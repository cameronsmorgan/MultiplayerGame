using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float bounceForce = 8f; // Stronger bounce effect
    [SerializeField] private string playerID;
    [SerializeField] private BoxCollider2D boundsCollider; // Restricts movement
    public Animator Snail1Cont; 

    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Start()
    {
        if (gameObject.name == "Player1")
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
            playerID = "Player1";
        }
        else if (gameObject.name == "Player2")
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard02", Keyboard.current);
            playerID = "Player2";
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Snail1Cont.SetBool("isMoving", false);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {

        Vector2 targetPosition = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;

        // If the new position is outside the paintable area, push the player back
        if (!PaintableAreaManager.Instance.CanPaint(targetPosition))
        {
            targetPosition = rb.position - (movementInput * moveSpeed * Time.fixedDeltaTime * 2.5f); // Push back
        }

        
        rb.MovePosition(targetPosition);

        if (movementInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }

        if (movementInput != Vector2.zero)
        {
            Snail1Cont.SetBool("isMoving", true);
        }
        else
        {
            Snail1Cont.SetBool("isMoving", false);
        }

        // Try to paint only in the allowed area
        PaintableAreaManager.Instance.PaintTile(targetPosition, playerID);
    }
}
