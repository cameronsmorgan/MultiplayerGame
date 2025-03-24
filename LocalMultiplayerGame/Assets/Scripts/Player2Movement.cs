using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Movement : MonoBehaviour
{
     public float moveSpeed = 5f;
    [SerializeField] private string playerID = "Player2"; // Default to Player 2
    [SerializeField] private BoxCollider2D boundsCollider; // Restricts movement
    public bool isBoosted;

    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Start()
    {
        GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard02", Keyboard.current);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // Try to paint only in the allowed area
        PaintableAreaManager.Instance.PaintTile(targetPosition, playerID);
    }
}
