using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movementInput;
    private Rigidbody2D rb;


    private void Start()
    {
        if (gameObject.name == "Player2")
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard02", Keyboard.current);
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    

    public void OnMove(InputAction.CallbackContext context)
    {
        // Read the input value from the Input System
        movementInput = context.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        // Calculate the target position
        Vector2 targetPosition = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;

        // Move the Rigidbody2D to the target position
        rb.MovePosition(targetPosition);
    }
}
